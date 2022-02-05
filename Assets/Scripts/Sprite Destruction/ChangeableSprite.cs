using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


//ATENTION: Make sure texture's import settings take the lowest quality we can afford. Aswell, format must be 32 bit
[RequireComponent(typeof(SpriteRenderer))]
public class ChangeableSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer
    {
        get
        {
            if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }

    SpriteRenderer _spriteRenderer;
    PolygonCollider2D _polygonCollider;

    Texture2D _texture;
    BitArray _bitArray;
    Color32[] _originalPixels;

    float _distanceUnit;
    Vector2Int lesserBound;
    Vector2Int greaterBound;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _distanceUnit = _spriteRenderer.bounds.size.x / _spriteRenderer.sprite.texture.width;

        _texture = new Texture2D(_spriteRenderer.sprite.texture.width, _spriteRenderer.sprite.texture.height, _spriteRenderer.sprite.texture.format, 1, true);

        _bitArray = new BitArray(_spriteRenderer.sprite.texture.width * _spriteRenderer.sprite.texture.height);
        _bitArray.SetAll(true);

        _originalPixels = _spriteRenderer.sprite.texture.GetPixels32();

        WipeOut();

    }

    /// <summary>
    /// Use this to make a sprite fully visible. CARE: Mind that transparent parts will be visible too.
    /// </summary>
    public void Restore()
    {
        _bitArray.SetAll(true);

        _texture.SetPixels32(_originalPixels);
        _texture.Apply();
        
        _spriteRenderer.sprite = Sprite.Create(_texture, _spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), _spriteRenderer.sprite.pixelsPerUnit);
    }

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
        /*
            Stopwatch locationWatch = new Stopwatch();
            locationWatch.Start();
            */

        #region Find Destroyed Pixels
        Vector2 localPosition = (worldPosition - transform.position);
        localPosition += new Vector2(_spriteRenderer.sprite.texture.width, _spriteRenderer.sprite.texture.height) * _distanceUnit * 0.5f;

        //Snap bounds
        lesserBound = new Vector2Int((int)Mathf.Round((localPosition.x - radius) / _distanceUnit), (int)Mathf.Round((localPosition.y - radius) / _distanceUnit));
        greaterBound = new Vector2Int((int)Mathf.Round((localPosition.x + radius) / _distanceUnit), (int)Mathf.Round((localPosition.y + radius) / _distanceUnit));

        if (lesserBound.x < 0)
            lesserBound.x = 0;

        if (lesserBound.y < 0)
            lesserBound.y = 0;


        if (greaterBound.x >= _spriteRenderer.sprite.texture.width)
            greaterBound.x = _spriteRenderer.sprite.texture.width;

        if (greaterBound.y >= _spriteRenderer.sprite.texture.height)
            greaterBound.y = _spriteRenderer.sprite.texture.height;

        bool dirty = false;

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

        //locationWatch.Stop();
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

        #region Update Collider
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

            //colliderWatch.Stop();
        }



        //UnityEngine.Debug.Log("Location : " + locationWatch.ElapsedMilliseconds);
        //UnityEngine.Debug.Log("Add Component: " + colliderWatch.ElapsedMilliseconds);
        //UnityEngine.Debug.Log("Modify: " + modifyWatch.ElapsedMilliseconds);

        #endregion
    }

}
