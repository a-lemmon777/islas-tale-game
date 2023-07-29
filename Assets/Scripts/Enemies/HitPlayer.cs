using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    /// <summary>
    /// Used to calculate damage source
    /// </summary>
    private Rigidbody2D _rigidbody2D;

    [Tooltip("How much HP is deducted per damage event")]
    public int DamageValue;

    [Tooltip("How long till another damage event in seconds")]
    public float Cooldown;

    /// <summary>
    /// The next time in seconds that the damage event can occur again
    /// </summary>
    private float _nextHitTime = float.NegativeInfinity;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Time.time < _nextHitTime)
            return;

        var health = other.gameObject.GetComponentInParent<MermaidHealth>();

        if (health == null) return;

        health.TakeDamage(damageValue: DamageValue,
            (_rigidbody2D.position - other.GetContact(0).point).x
        );

        _nextHitTime = Time.time + Cooldown;

    }
}
