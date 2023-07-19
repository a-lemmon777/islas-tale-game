using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StarfishController : MonoBehaviour
{
    [Tooltip("Speed in units per second")]
    public float Speed = 20f;

    [Tooltip("Rate of spin in degrees per second")]
    public float AngularVelocity = -720f;

    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.right * Speed;
        _rigidbody2D.angularVelocity = AngularVelocity;
        // Destroy the starfish after 3 seconds to save memory.
        Destroy(gameObject, 3f);
    }

    public void SetDirection(Vector2 direction)
    {
        _rigidbody2D.velocity = direction.normalized * Speed;
    }
}
