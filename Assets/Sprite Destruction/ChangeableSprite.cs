using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


//ATENTION: Make sure texture's import settings take the lowest quality we can afford. Aswell, format must be 32 bit
[RequireComponent(typeof(SpriteRenderer))]
public class ChangeableSprite : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool _startTransparent;

    public SpriteRenderer spriteRenderer
    {
        get
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }

    public bool blockChanges;

    SpriteRenderer _spriteRenderer;
    PolygonCollider2D _polygonCollider;

    Texture2D _texture;
    BitArray _bitArray;
    Color32[] _originalPixels;

    Vector2 _distanceUnit;
    Vector2Int lesserBound;
    Vector2Int greaterBound;

    Vector2 zeroPoint;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();

        var originalAngle = transform.eulerAngles;

        transform.eulerAngles = Vector3.zero;
        _distanceUnit.x = _spriteRenderer.bounds.size.x / _spriteRenderer.sprite.texture.width;
        _distanceUnit.y = _spriteRenderer.bounds.size.y / _spriteRenderer.sprite.texture.height;

        transform.eulerAngles = originalAngle;

        _texture = new Texture2D(_spriteRenderer.sprite.texture.width, _spriteRenderer.sprite.texture.height, _spriteRenderer.sprite.texture.format, 1, true);

        _bitArray = new BitArray(_spriteRenderer.sprite.texture.width * _spriteRenderer.sprite.texture.height);
        _bitArray.SetAll(true);

        UpdateOriginalPixels();

        zeroPoint = -new Vector2(_spriteRenderer.sprite.texture.width, _spriteRenderer.sprite.texture.height) * _distanceUnit * 0.5f;

        if (_startTransparent) WipeOut();

    }

    /// <summary>
    /// Use this to make a sprite fully visible.
    /// </summary>
    public void Restore()
    {
        _bitArray.SetAll(true);


        _texture.SetPixels32(_originalPixels);
        _texture.Apply();

        _spriteRenderer.sprite = Sprite.Create(_texture, _spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), _spriteRenderer.sprite.pixelsPerUnit);
    }

    /// <summary>
    /// Use this to make a sprite invisible.
    /// </summary>
    public void WipeOut()
    {
        var pixels = _spriteRenderer.sprite.texture.GetPixels();

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].a = 0;
            _bitArray[i] = false;
        }

        _texture.SetPixels(pixels);
        _texture.Apply();

        _spriteRenderer.sprite = Sprite.Create(_texture, _spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), _spriteRenderer.sprite.pixelsPerUnit);
    }

    //Testing
    [ContextMenu("Hurt in center")]
    private void Hurt()
    {
        Destroy(transform.position, 0.5f);
    }

    public void Create(Vector3 worldPosition, float radius)
    {
        //throw new NotImplementedException();
        Modify(worldPosition, radius, true);
    }


    public void Destroy(Vector3 worldPosition, float radius)
    {
        Modify(worldPosition, radius, false);
    }

    void Modify(Vector3 worldPosition, float radius, bool value)
    {
        if (blockChanges) return;

        /*
            Stopwatch locationWatch = new Stopwatch();
            locationWatch.Start();
            */

        #region Find Destroyed Pixels
        Vector2 localPosition = transform.InverseTransformPoint(worldPosition);

        localPosition = localPosition - zeroPoint;

        //Snap bounds
        lesserBound = new Vector2Int((int)Mathf.Round((localPosition.x - radius) / _distanceUnit.x), (int)Mathf.Round((localPosition.y - radius) / _distanceUnit.y));
        greaterBound = new Vector2Int((int)Mathf.Round((localPosition.x + radius) / _distanceUnit.x), (int)Mathf.Round((localPosition.y + radius) / _distanceUnit.y));

        if (lesserBound.x < 0)
            lesserBound.x = 0;

        if (lesserBound.y < 0)
            lesserBound.y = 0;


        if (greaterBound.x > _spriteRenderer.sprite.texture.width)
            greaterBound.x = _spriteRenderer.sprite.texture.width;

        if (greaterBound.y > _spriteRenderer.sprite.texture.height)
            greaterBound.y = _spriteRenderer.sprite.texture.height;

        bool dirty = false;

        //Power 2 so it can be compared with squaredMagnitude instead of magnitude(distance)
        radius = radius * radius;
        for (int i = lesserBound.x; i < greaterBound.x; i++)
        {
            for (int j = lesserBound.y; j < greaterBound.y; j++)
            {
                if (_bitArray[i + _spriteRenderer.sprite.texture.width * j] != value)
                {
                    if (Vector2.SqrMagnitude(localPosition - new Vector2(i, j) * _distanceUnit) < radius)
                    {
                        _bitArray[i + _spriteRenderer.sprite.texture.width * j] = value;
                        dirty = true;
                    }
                }
            }
        }

        #endregion

        if (!dirty) return;

        #region Update Pixels
        //Stopwatch modifyWatch = new Stopwatch();
        //modifyWatch.Start();
        var pixels = _spriteRenderer.sprite.texture.GetPixels32();


        for (int i = 0; i < pixels.Length; i++)
        {
            if (_bitArray[i])
            {
                pixels[i] = _originalPixels[i];
            }
            else
            {
                pixels[i].a = 0;
            }
        }

        _texture.SetPixels32(pixels);
        _texture.Apply();

        _spriteRenderer.sprite = Sprite.Create(_texture, _spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), _spriteRenderer.sprite.pixelsPerUnit);
        //modifyWatch.Stop();
        #endregion


        UpdateCollider();
    }

    public void UpdateCollider()
    {
        if (_polygonCollider)
        {
            //Stopwatch colliderWatch = new Stopwatch();
            //colliderWatch.Start();
            Destroy(_polygonCollider);
            _polygonCollider = gameObject.AddComponent<PolygonCollider2D>();


            /*This doesnt work somehow. It would be nice to use this instead of adding a new Collider.
            _polygonCollider.pathCount = _spriteRenderer.sprite.GetPhysicsShapeCount();
            List<Vector2> path = new List<Vector2>();

            for (int i = 0; i < _polygonCollider.pathCount; i++)
            {
                path.Clear();
                _spriteRenderer.sprite.GetPhysicsShape(i, path);

                _polygonCollider.SetPath(i, path.ToArray());
            }
            */

        }
    }

    public void AddCollider()
    {
        if (_polygonCollider == null)
        {
            _polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        }
    }

    public void UpdateOriginalPixels()
    {
        _originalPixels = _spriteRenderer.sprite.texture.GetPixels32();

    }

}
