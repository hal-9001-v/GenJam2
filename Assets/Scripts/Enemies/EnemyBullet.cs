using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : Enemy
{
    Health _health;
    Hurter _hurter;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _hurter = GetComponent<Hurter>();

        _health.deadAction += Die;
        _hurter.hitSuccessAction += (health, collider) => { Die(); };
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void EnemyMovement()
    {

    }

    protected override void GotHurt()
    {

    }

}
