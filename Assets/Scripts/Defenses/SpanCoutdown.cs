using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanCoutdown : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(1, 20)] float _time;

    public Action endOfCountdown;

    private void Start()
    {
        StartCoroutine(LifeCountDown());
    }

    IEnumerator LifeCountDown()
    {
        yield return new WaitForSeconds(_time);


        if (endOfCountdown != null)
        {
            endOfCountdown.Invoke();
        }
    }

    public void RestartCountdow()
    {
        StopAllCoroutines();

        StartCoroutine(LifeCountDown());
    }
}
