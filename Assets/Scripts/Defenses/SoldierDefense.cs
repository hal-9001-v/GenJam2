using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDefense : BaseDefense
{

    private void Awake()
    {
        Initialize();
        type = RulerType.Type2;
        _animator =  GetComponent<Animator>();
    }

    override protected void Die()
    {
        StartCoroutine(Shake.DOShake(.15f, .3f,FindObjectOfType<Camera>().transform));  
        Destroy(gameObject);
    }

}
