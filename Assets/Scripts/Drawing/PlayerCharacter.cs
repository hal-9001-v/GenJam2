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

    private void Awake()
    {
        //gameObject.tag = "Drawing";
        _health = GetComponent<Health>();
        _pulsator = FindObjectOfType<Pulsator>();

        _endgame = FindObjectOfType<EndGame>();

        _endgame.retryCallback += _health.Restore;
    }

    void Die()
    {
        _pulsator.HurtAnimation();
    }

    void Hurt()
    {
        _pulsator.HurtAnimation();
    }

    /*
    private void OnTriggerEnter2D(Collider2D other) {
        
   
       Debug.Log("Collision with" + other.gameObject.tag);
       switch(other.gameObject.tag){

           case "EnemyTest": 
                Hurt(other.gameObject.GetComponent<EnemyTest>().damage);
                Destroy(other.gameObject);
                FindObjectOfType<Pulsator>().HurtAnimation();
           break;


           default: Hurt(1);
           break;
       }

    }*/





}
