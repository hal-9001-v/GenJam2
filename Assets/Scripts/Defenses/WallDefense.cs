using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChangeableSprite))]
[RequireComponent(typeof(PolygonCollider2D))]
public class WallDefense : BaseDefense
{
    Health _health;

    SpanCoutdown _spanCountdown;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _spanCountdown = GetComponent<SpanCoutdown>();

        _health.deadAction += Die;
        _spanCountdown.endOfCountdown += Die;
    }


    protected override void Die()
    {
        Destroy(gameObject);
    }

}
