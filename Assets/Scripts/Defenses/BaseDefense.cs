using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(SpanCoutdown))]
public abstract class BaseDefense : MonoBehaviour
{

    protected Health _health;
    protected SpanCoutdown _spanCountdown;
    protected RulerType type;
    protected Animator _animator;

    public Animator GetAnimator()
    {

        return _animator;

    }
    protected void Initialize()
    {
        _health = GetComponent<Health>();
        _spanCountdown = GetComponent<SpanCoutdown>();
        _health.deadAction += Die;
        _health.hurtAction += Hurt;
        _spanCountdown.endOfCountdown += Die;

    }
    public RulerType GetRulerType()
    {

        return type;

    }
    protected abstract void Die();


    void Hurt()
    {
        StartCoroutine(Shake.DOShake(.15f, .3f, FindObjectOfType<Camera>().transform));
    }

    public abstract void Improve();

}
