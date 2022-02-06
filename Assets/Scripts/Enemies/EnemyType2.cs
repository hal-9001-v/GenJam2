using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : Enemy
{
    
    Vector3 desiredDirection; 
    float elapsedTime;
    [SerializeField] private float oscillation;
    [SerializeField] private float period;
    private void Awake() {
        elapsedTime = 0;
        damage = 1;
        health = 1;
        totalHealth = health;
        speed = 10000;
        gameObject.tag = "EnemyType2"; 
        enemySpawner = FindObjectOfType<MainDrawingSpawner>();
        rb2D = GetComponent<Rigidbody2D>();  
    }

    private void Start() {
        var target = enemySpawner.transform.position;    
        desiredDirection = (target - transform.position).normalized;
    }
    private void Update() {
        EnemyMovement();
    }
    override  public void Hurt(){
        Destroy(this.gameObject);
    }

    override public void EnemyMovement(){
       
        elapsedTime += Time.deltaTime;
        float step = speed * Time.deltaTime;
        var desiredVelocity = (Vector2) desiredDirection*step - rb2D.velocity;
        rb2D.AddForce(desiredVelocity, ForceMode2D.Impulse);


    }

}
