using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class MainDrawingCharacter : BaseDefense
{


    private void Awake() {
        gameObject.tag = "Drawing";
        health = 10;
    }   

    override protected void Die(){

      Debug.Log("La palmas");

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
