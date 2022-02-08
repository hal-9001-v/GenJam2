using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleBlink : MonoBehaviour
{

    [Header("Blinking Values")]
    [SerializeField] private float growValue;
    [SerializeField] private float growDuration;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(growValue, growDuration).SetLoops(-1,LoopType.Yoyo).SetId("GrowAnimation");
    }
    private void OnDestroy()
    {
        DOTween.Kill("GrowAnimation");
    }
}
