using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : Enemy
{
    
    Vector3 desiredDirection; 
    float elapsedTime;
    [SerializeField] private float oscillation;
    [SerializeField] private float period;
    private void Awake() {
        elapsedTime = 0;
        damage = 3;
        speed = 5000;
        health = 2;
        totalHealth = health;
        gameObject.tag = "EnemyType3"; 
        enemySpawner = FindObjectOfType<MainDrawingSpawner>();
        rb2D = GetComponent<Rigidbody2D>();  
    }

    private void Start() {
    }
    private void Update() {
        EnemyMovement();
    }

    override public void Hurt(){

        health--;
        rb2D.AddForce(-transform.forward*Time.deltaTime*50000f);
        if(health <= 0){

            Die();

        }

    }

    private void Die(){

        Destroy(gameObject);

    }

    override public void EnemyMovement(){
       
        elapsedTime += Time.deltaTime;
        float step = speed * Time.deltaTime;
        var target = enemySpawner.transform.position;    
        desiredDirection = (target - transform.position).normalized;
        var cross = Vector3.Cross(desiredDirection, Vector3.forward)*oscillation;
        var desiredVelocity = (Vector2) desiredDirection*step - rb2D.velocity + 
        (Vector2) (Mathf.Sin(elapsedTime)*period * cross);
        rb2D.AddForce(desiredVelocity, ForceMode2D.Impulse);


    }

}
