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

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckForPlayerCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckForPlayerCollision(collision);
    }

    private void CheckForPlayerCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var health = collision.gameObject.GetComponentInParent<MermaidHealth>();

            if (health == null) return;

            health.TakeDamage(damageValue: DamageValue,
                (_rigidbody2D.position - collision.GetContact(0).point).x
            );
        }
    }
}
