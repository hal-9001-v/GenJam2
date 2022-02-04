using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class MainDrawingCharacter : MonoBehaviour
{
    [SerializeField ]private int health;


    private void Awake() {
        gameObject.tag = "Drawing";
        health = 10;
    }   

    private void Hurt(int dmg){

        if(health-dmg > 0) health -= dmg;
        else health = 0;

        if(health == 0) Die();

    }

    private void Die(){

        Debug.Log("La has palma'o");
        Time.timeScale = 0f;

    }

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

    }

       
        
     
}
