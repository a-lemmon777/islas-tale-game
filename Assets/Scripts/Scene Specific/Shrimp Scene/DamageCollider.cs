using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class DamageCollider : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.gameObject.GetComponent<ShrimpHealth>();
        health.TakeDamage(1,
            (_rigidbody2D.position - other.GetContact(0).point).x
        );
    }
}
