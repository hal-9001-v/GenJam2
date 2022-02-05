using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDefense : BaseDefense
{   
   private void Awake() {
        gameObject.tag = "Defense";
       lifespan = 2f;
       health = 1;
   }
    private void Start() {
        StartCoroutine(DestroyOnElapsed());
    }  

     override protected void Die(){

        Destroy(father);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
   
        Debug.Log("Collision with" + other.gameObject.tag);
       switch(other.gameObject.tag){

           case "EnemyTest": 
                Hurt(other.gameObject.GetComponent<EnemyTest>().damage);
                Destroy(other.gameObject);
           break;


           default: Hurt(1);
           break;
       }

    }
}
