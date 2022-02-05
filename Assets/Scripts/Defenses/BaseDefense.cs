using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BaseDefense : MonoBehaviour
{
    public float lifespan;
    public int health;
    public GameObject father;

    public IEnumerator DestroyOnElapsed(){
        yield return new WaitForSeconds(lifespan);
        Destroy(father);

    }   

    protected void Hurt(int dmg){

        if(health-dmg > 0) health -= dmg;
        else health = 0;

        if(health == 0) Die();

    }

    protected abstract void Die();
}
