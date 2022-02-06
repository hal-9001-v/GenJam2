using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class Enemy : MonoBehaviour
{

    public int health;    
    public int totalHealth;    
    public float speed;
    public int damage;
    public MainDrawingSpawner enemySpawner;
    public Rigidbody2D rb2D;
    
    
    public abstract void EnemyMovement();
    public abstract void Hurt();

    void OnDestroy() {
        
    } 

}
