using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Pulsator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0.1f, 2)] float animationDuration = 0.4f;
    [SerializeField] [Range(0.1f, 2)] float animationScale = 0.7f;

    [Space(5)]
    [SerializeField] [Range(0.1f, 2)] float hurtDuration = 0.15f;
    [SerializeField] [Range(0.1f, 2)] float hurtScale = 1.2f;


    SpriteRenderer spriteRenderer;

    Vector3 _startingScale;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _startingScale = transform.localScale;

    }

    private void Start()
    {
        StartAnimation();
    }
    public void StartAnimation()
    {
        transform.DOScale(animationScale * _startingScale, animationDuration).SetLoops(-1, LoopType.Yoyo).SetId("CenterAnim");
        spriteRenderer.DOColor(new Vector4(0.58f, 1, 0.51f, 11), 0.4f).SetLoops(-1, LoopType.Yoyo).SetId("CenterAnim");
    }
    public void HurtAnimation()
    {
        DOTween.PauseAll();
        transform.DOScale(hurtScale * _startingScale, hurtDuration).SetLoops(2, LoopType.Yoyo).SetId("CenterHurt");
        spriteRenderer.DOColor(new Vector4(1, 0.58f, 0.51f, 11), 0.15f).SetLoops(2, LoopType.Yoyo).SetId("CenterHurt");
        ResumeAnimation();
    }
    public void ResumeAnimation()
    {
        DOTween.Play("CenterAnim");

    }

}
