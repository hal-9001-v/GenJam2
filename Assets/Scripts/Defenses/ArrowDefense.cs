using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowDefense : BaseDefense
{

    private void Awake()
    {
        Initialize();
        _spanCountdown.endOfCountdown += Die;
        type = RulerType.Type3;
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }

}
