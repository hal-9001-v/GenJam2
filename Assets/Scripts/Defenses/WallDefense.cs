using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class WallDefense : BaseDefense
{   
   private void Awake() {
        gameObject.tag = "WallDefense";
       lifespan = 2f;
       health = 1;
        totalHealth = health;
   }
    private void Start() {
        //StartCoroutine(DestroyOnElapsed());
    }  

     override protected void Die(){

        Destroy(father);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
   
        //Debug.Log("Collision with" + other.gameObject.tag);
       switch(other.gameObject.tag){

           case "EnemyType1": 
           case "EnemyType2": 
                Hurt(other.gameObject.GetComponent<Enemy>().damage);
                other.gameObject.GetComponent<Enemy>().Hurt();
            break;
           case "EnemyType3":  
                Hurt(other.gameObject.GetComponent<Enemy>().damage);
           break;

            case "Arrow": return;
           default: Hurt(1);
           break;
       }

    }
=======
[RequireComponent(typeof(ChangeableSprite))]
[RequireComponent(typeof(PolygonCollider2D))]
public class WallDefense : BaseDefense
{
    Health _health;

    SpanCoutdown _spanCountdown;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _spanCountdown = GetComponent<SpanCoutdown>();

        _health.deadAction += Die;
        _spanCountdown.endOfCountdown += Die;
    }


    protected override void Die()
    {
        Destroy(gameObject);
    }

>>>>>>> PuttingThingsTogether
}
