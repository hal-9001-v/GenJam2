using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDestroyer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0.1f, 1)] float _radius;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var destructible = collision.gameObject.GetComponent<DestructibleSprite>();
        if (destructible)
        {
            destructible.Hurt(collision.ClosestPoint(transform.position), _radius);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
