using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Enemy
{
    
    Vector3 desiredDirection; 
    float elapsedTime;
    [SerializeField] private float oscillation;
    [SerializeField] private float period;
    private void Awake() {
        elapsedTime = 0;
        damage = 1;
        speed = 5000;
        gameObject.tag = "EnemyTest"; 
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

    override public void EnemyMovement(){
       
        elapsedTime += Time.deltaTime;
        float step = speed * Time.deltaTime;
        var cross = Vector3.Cross(desiredDirection, Vector3.forward)*oscillation;
        var desiredVelocity = (Vector2) desiredDirection*step - rb2D.velocity + 
        (Vector2) (Mathf.Sin(elapsedTime*period) * cross);
        rb2D.AddForce(desiredVelocity, ForceMode2D.Impulse);


    }

}
