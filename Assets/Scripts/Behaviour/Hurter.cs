using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<HealthTag> _targets;
    [SerializeField] [Range(1, 5)] int _damage;

    public Action<Health> hitSuccessAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<Health>();

        if (health)
        {
            if (_targets.Contains(health.healthTag))
            {

                health.Hurt(_damage);

                if (hitSuccessAction != null)
                {
                    hitSuccessAction.Invoke(health);
                }
            }
        }
    }

}
