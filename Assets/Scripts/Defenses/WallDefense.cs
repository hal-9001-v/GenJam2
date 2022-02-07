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
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

}