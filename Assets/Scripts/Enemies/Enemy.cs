using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public abstract class Enemy : MonoBehaviour
{
    
    public float speed;
    public int damage;
    public MainDrawingSpawner enemySpawner;
    public Rigidbody2D rb2D;
    
    
    public abstract void EnemyMovement();

    void OnDestroy() {
        
    } 

}
