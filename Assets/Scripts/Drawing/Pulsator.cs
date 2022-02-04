using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Pulsator : MonoBehaviour
{   
    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        StartAnimation();
    }
    public void StartAnimation() {
        transform.DOScale(0.7f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetId("CenterAnim");
        spriteRenderer.DOColor(new Vector4(0.58f, 1, 0.51f,11), 0.4f).SetLoops(-1, LoopType.Yoyo).SetId("CenterAnim");
    }
    public void HurtAnimation(){
        DOTween.PauseAll();
        transform.DOScale(1.2f, 0.15f).SetLoops(2,LoopType.Yoyo).SetId("CenterHurt");
        spriteRenderer.DOColor(new Vector4(1, 0.58f, 0.51f,11), 0.15f).SetLoops(2,LoopType.Yoyo).SetId("CenterHurt");
        ResumeAnimation();
    }
    public void ResumeAnimation(){
        DOTween.Play("CenterAnim");    
            
    }

}
