using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEnemy : Enemy
{
    [Header("Settings")]
    [SerializeField] private float oscillation;
    [SerializeField] private float period;

    Vector3 desiredDirection;
    float elapsedTime;

    PlayerCharacter _target;

    float originalX;

    private void Awake()
    {
        Initialize();
        elapsedTime = 0;
        originalX = transform.localScale.x;
    }

    private void Start()
    {
        _target = FindObjectOfType<PlayerCharacter>();

    }
    private void FixedUpdate()
    {
        EnemyMovement();
    }

    override public void EnemyMovement()
    {

        desiredDirection = _target.transform.position - transform.position;
        desiredDirection.Normalize();

        //Flip
        if (desiredDirection.x > 0)
        {
            devilFlipper.InverseFlip();
        }
        else
        {
            devilFlipper.RightFlip();
        }

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
