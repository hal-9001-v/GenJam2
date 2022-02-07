using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hurter))]
public abstract class Enemy : MonoBehaviour
{
<<<<<<< HEAD

    public int health;    
    public int totalHealth;    
    public float speed;
    public int damage;
    public MainDrawingSpawner enemySpawner;
    public Rigidbody2D rb2D;
    
    
    public abstract void EnemyMovement();
    public abstract void Hurt();
=======
>>>>>>> PuttingThingsTogether

    [SerializeField] [Range(1, 20)] protected float speed;
        
    protected Rigidbody2D rb2D;
    protected Hurter hurter;
    protected Health health;

    protected void Initialize()
    {
        hurter = GetComponent<Hurter>();
        health = GetComponent<Health>();
        rb2D = GetComponent<Rigidbody2D>();

        health.deadAction += Die;
        health.hurtAction += GotHurt;

        hurter.hitSuccessAction += (target, collider) => { Die(); };

    }

    public abstract void Die();

    protected abstract void GotHurt();

    public abstract void EnemyMovement();

}
