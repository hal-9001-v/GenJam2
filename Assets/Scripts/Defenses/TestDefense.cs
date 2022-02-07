using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestDefense : BaseDefense
{

    private void Awake()
    {
        var span = GetComponent<SpanCoutdown>();

        span.endOfCountdown += () =>
        {
            Die();
        };
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

}
