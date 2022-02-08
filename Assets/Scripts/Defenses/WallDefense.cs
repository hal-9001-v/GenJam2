using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangeableSprite))]
[RequireComponent(typeof(Collider2D))]
public class WallDefense : BaseDefense
{

    private void Awake()
    {
        
        Initialize();
        type = RulerType.Type1;
    }

    protected override void Die()
    {
        StartCoroutine(Shake.DOShake(.15f, .3f,FindObjectOfType<Camera>().transform));  
        Destroy(gameObject);
    }

}