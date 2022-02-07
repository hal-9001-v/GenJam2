using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/Scripts/Enemies/EnemyType1.cs
public class EnemyType1 : Enemy
=======

public class EnemyTest : Enemy

>>>>>>> PuttingThingsTogether:Assets/Scripts/Enemies/EnemyTest.cs
{
    [Header("Settings")]
    [SerializeField] private float oscillation;
    [SerializeField] private float period;

    Vector3 desiredDirection;
    float elapsedTime;

    private void Awake()
    {
        Initialize();

        elapsedTime = 0;
<<<<<<< HEAD:Assets/Scripts/Enemies/EnemyType1.cs
        damage = 1;
        health = 1;
        totalHealth = health;
        
        speed = 5000;
        gameObject.tag = "EnemyType1"; 
        enemySpawner = FindObjectOfType<MainDrawingSpawner>();
        rb2D = GetComponent<Rigidbody2D>();  
=======
>>>>>>> PuttingThingsTogether:Assets/Scripts/Enemies/EnemyTest.cs
    }

    private void Start()
    {

        var target = FindObjectOfType<PlayerCharacter>();
        if (target)
        {
            desiredDirection = (target.transform.position - transform.position).normalized;
        }
    }
    private void FixedUpdate()
    {
        EnemyMovement();
    }   

    override public void Hurt(){
        Destroy(this.gameObject);
    }

    override public void EnemyMovement()
    {

        elapsedTime += Time.deltaTime;

        var cross = Vector3.Cross(desiredDirection, Vector3.forward) * oscillation;

        var desiredVelocity = (Vector2)desiredDirection * speed - rb2D.velocity;

        desiredVelocity += (Vector2)cross * Mathf.Sin(elapsedTime * period);

        rb2D.AddForce(desiredVelocity, ForceMode2D.Impulse);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    protected override void GotHurt()
    {

    }
}
