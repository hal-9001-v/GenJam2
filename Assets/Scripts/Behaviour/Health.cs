using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(1, 10)] int _maxHealthPoins = 1;

    public int maxHealthPoins { get { return _maxHealthPoins; } }

    public int currentHealthPoints { get; private set; }

    /// <summary>
    /// Action called whenever this component is damaged. It won't happen when it reaches 0, for deadAction is called then
    /// </summary>
    public Action hurtAction;

    /// <summary>
    /// This action is called when dead
    /// </summary>
    public Action deadAction;

    [SerializeField] HealthTag _healthTag;
    public HealthTag healthTag { get { return _healthTag; } }

    private void Awake()
    {
        Restore();
    }

    /// <summary>
    /// Hurt one point
    /// </summary>
    public void Hurt()
    {
        Hurt(1);
    }
    
    public void Hurt(int damage)
    {
        //Is it already dead?
        if (currentHealthPoints <= 0) return;


        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            if (deadAction != null)
            {
                deadAction.Invoke();
            }
        }
        else
        {
            if (hurtAction != null)
            {
                hurtAction.Invoke();
            }
        }
    }

    public void Restore()
    {
        currentHealthPoints = _maxHealthPoins;
    }

    public void ModifyMaxHealth(int newMaxHealth)
    {
        _maxHealthPoins = newMaxHealth;

        Restore();
    }

}
