using System;
using UnityEngine;

public class SpriteDestroyer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(0.1f, 5)] float _radius;
    [SerializeField] [Range(0f, 5)] float _biteDistance;

    [SerializeField] bool _hitOnlyOnce = true;
    bool _done;

    public Action<ChangeableSprite> hitCallback;

    /// <summary>
    /// Check if Collider has a changeableSprite and hurts it.
    /// </summary>
    /// <param name="collider"></param>
    public bool TryHurtSprite(Collider2D collider)
    {
        if (_hitOnlyOnce && _done) return false;

        var destructible = collider.gameObject.GetComponent<ChangeableSprite>();
        if (destructible)
        {
            _done = true;
            BiteDestruction(destructible, collider.ClosestPoint(transform.position));


            if (hitCallback != null)
            {
                hitCallback.Invoke(destructible);
            }

            return true;
        }

        return false;
    }

    void BiteDestruction(ChangeableSprite sprite, Vector3 collisionPoint)
    {
        var direction = collisionPoint - transform.position;
        direction.Normalize();


        Vector3 relativeUp = Vector3.Cross(direction, Vector3.forward);

        sprite.Destroy(collisionPoint + direction * 0.5f * _biteDistance, _radius);
        sprite.Destroy(collisionPoint + relativeUp * _biteDistance, _radius);
        sprite.Destroy(collisionPoint - relativeUp * _biteDistance, _radius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f * _biteDistance, _radius);
        Gizmos.DrawWireSphere(transform.position + Vector3.right * _biteDistance, _radius);
        Gizmos.DrawWireSphere(transform.position - Vector3.right * _biteDistance, _radius);

    }
}
