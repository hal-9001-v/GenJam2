using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]

public class PlayerCharacter : MonoBehaviour
{

    Health _health;
    Pulsator _pulsator;

    EndGame _endgame;

    HealthCrossDrawer healthCrossDrawer;


    private void Awake()
    {
        healthCrossDrawer = FindObjectOfType<HealthCrossDrawer>();

        //gameObject.tag = "Drawing";
        _health = GetComponent<Health>();
        _pulsator = FindObjectOfType<Pulsator>();

        _health.hurtAction += Hurt;
    }
       private void Start() {
            _endgame = FindObjectOfType<EndGame>();
            _endgame.retryCallback += _health.Restore;
            _endgame.retryCallback += healthCrossDrawer.Restore;
       }

    void Die()
    {
        _pulsator.HurtAnimation();
    }

    void Hurt()
    {
        StartCoroutine(Shake.DOShake(.20f, .5f,FindObjectOfType<Camera>().transform));  
        _pulsator.HurtAnimation();


    }





}
