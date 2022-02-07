using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpanCoutdown))]
public abstract class BaseDefense : MonoBehaviour
{
<<<<<<< HEAD
    public float lifespan;
    public int totalHealth;
    public int health;
    public GameObject father;


    public IEnumerator DestroyOnElapsed(){
        yield return new WaitForSeconds(lifespan);
        Destroy(father);
      
    }   
    
    protected void Hurt(int dmg){

        if(health-dmg > 0) health -= dmg;
        else health = 0;

        if(health == 0) {
            Die();
            DOTween.Kill("ImprovingAnim");
        }

    }

=======
>>>>>>> PuttingThingsTogether
    protected abstract void Die();
}
