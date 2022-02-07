using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Hurter))]
[RequireComponent(typeof(SpanCoutdown))]
public class Arrow : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(1, 20)] private float _speed;
    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();

        var spanCountdown = GetComponent<SpanCoutdown>();
        spanCountdown.endOfCountdown += Die;
    }

    private void Update()
    {
        var desiredVelocity = (Vector2)transform.up * _speed - _rb2D.velocity;
        _rb2D.AddForce(desiredVelocity, ForceMode2D.Impulse);
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
