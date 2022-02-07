using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowDefense : BaseDefense
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
