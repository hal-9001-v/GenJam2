using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDefense : BaseDefense
{

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        // StartCoroutine(DestroyOnElapsed());
    }

    override protected void Die()
    {
        Destroy(gameObject);
    }

}
