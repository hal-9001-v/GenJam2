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

    protected void Initialize()
    {
        _health = GetComponent<Health>();
        _spanCountdown = GetComponent<SpanCoutdown>();

        _health.deadAction += Die;
        _spanCountdown.endOfCountdown += Die;
    }

    protected abstract void Die();

    public void ImproveHealth(int newMaxHealth)
    {
        var health = GetComponent<Health>();

        health.ModifyMaxHealth(newMaxHealth);
    }

}
