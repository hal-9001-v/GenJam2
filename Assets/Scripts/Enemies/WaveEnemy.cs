using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : Enemy
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
    }

    private void Start()
    {

        var target = FindObjectOfType<PlayerCharacter>();
        if (target)
        {
            desiredDirection = (target.transform.position - transform.position).normalized;

            if (desiredDirection.x > 0)
            {
                devilFlipper.InverseFlip();
            }
        }
    }
    private void FixedUpdate()
    {
        EnemyMovement();
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
        DeathEffect();

        StartCoroutine(DeathCountDown());
    }

    protected override void GotHurt()
    {

    }
}
