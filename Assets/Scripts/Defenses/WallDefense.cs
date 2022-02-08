using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangeableSprite))]
public class WallDefense : BaseDefense
{
    [Header("Settings")]
    [SerializeField] [Range(1, 10)] int _improvedHealth;
    [SerializeField] Sprite _improvedSprite;

    ChangeableSprite _changeableSprite;
    SpriteRenderer _spriteRenderer;


    private void Awake()
    {

        Initialize();
        type = RulerType.Type1;

        _changeableSprite = GetComponent<ChangeableSprite>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spanCountdown = GetComponent<SpanCoutdown>();

    }

    protected override void Die()
    {
        StartCoroutine(Shake.DOShake(.15f, .3f, FindObjectOfType<Camera>().transform));
        Destroy(gameObject);
    }

    [ContextMenu("Improve")]
    public override void Improve()
    {
        _changeableSprite.blockChanges = false;
        _health.ModifyMaxHealth(_improvedHealth);

        _spriteRenderer.sprite = _improvedSprite;
        
        _changeableSprite.UpdateOriginalPixels();
        _changeableSprite.Restore();
        _changeableSprite.UpdateCollider();


        _spanCountdown.RestartCountdow();
    }
}