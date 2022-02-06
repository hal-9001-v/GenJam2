using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDefense : BaseDefense
{   
   private void Awake() {
        gameObject.tag = "ShootingDefense";
       lifespan = 0.2f;
       health = 1;
        totalHealth = health;
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

           case "EnemyType1": 
           case "EnemyType2": 
           case "EnemyType3": 
                Hurt(other.gameObject.GetComponent<Enemy>().damage);
                other.gameObject.GetComponent<Enemy>().Hurt();
           break;

           case "Arrow": return;
           default: Hurt(1);
           break;
       }

    }
}
