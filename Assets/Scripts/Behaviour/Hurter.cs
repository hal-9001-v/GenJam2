using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<WeightedTarget> _targets;
    [SerializeField] [Range(1, 5)] int _damage;
    [SerializeField] SpriteDestroyer _spriteDestroyer;

    public Action<Health, Collider2D> hitSuccessAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<Health>();

        if (health)
        {
            foreach (var weightedTarget in _targets)
            {
                if (weightedTarget.target == health.healthTag)
                {
                    health.Hurt(_damage * weightedTarget.damageScale);

                    if (_spriteDestroyer)
                    {
                        _spriteDestroyer.TryHurtSprite(collision);
                    }

                    if (hitSuccessAction != null)
                    {
                        hitSuccessAction.Invoke(health, collision);
                    }
                }
            }
        }
    }

    [Serializable]
    class WeightedTarget
    {
        public HealthTag target;
        [Range(0, 5)] public int damageScale = 1;
    }

}
