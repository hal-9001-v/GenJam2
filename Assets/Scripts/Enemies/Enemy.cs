using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Hurter))]
public abstract class Enemy : MonoBehaviour
{

    [SerializeField] [Range(1, 20)] protected float speed;
        
    protected Rigidbody2D rb2D;
    protected Hurter hurter;
    protected Health health;
    protected new ParticleSystem particleSystem;
    protected Collider2D collider;
    protected SpriteRenderer spriteRenderer;


    protected void Initialize()
    {
        hurter = GetComponent<Hurter>();
        health = GetComponent<Health>();
        rb2D = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        health.deadAction += Die;
        health.hurtAction += GotHurt;

        hurter.hitSuccessAction += (target, collider) => { Die(); };
    }


    public void DeathEffect()
    {
        particleSystem.Emit(5);
        particleSystem.Play();
        collider.enabled = false;

        spriteRenderer.enabled = false;
    }

    protected IEnumerator DeathCountDown()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public abstract void Die();

    protected abstract void GotHurt();

    public abstract void EnemyMovement();

}
