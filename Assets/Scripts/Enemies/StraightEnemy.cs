using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightEnemy : Enemy
{

    Vector3 desiredDirection;
    
    private void Awake()
    {
        Initialize();

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

    override public void EnemyMovement()
    {
        var desiredVelocity = (Vector2)desiredDirection * speed - rb2D.velocity;

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
