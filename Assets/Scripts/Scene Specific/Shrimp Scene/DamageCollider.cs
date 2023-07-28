using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class DamageCollider : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public int DamageValue;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var health = other.gameObject.GetComponentInParent<ShrimpHealth>();

        health.TakeDamage(damageValue: DamageValue,
            (_rigidbody2D.position - other.GetContact(0).point).x
        );

        Destroy(gameObject);
    }
}
