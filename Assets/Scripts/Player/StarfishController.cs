using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StarfishController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed in units per second")]
    private float Speed = 20f;

    [SerializeField]
    [Tooltip("Rate of spin in degrees per second")]
    private float AngularVelocity = -720f;

    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.right * Speed;
        _rigidbody2D.angularVelocity = AngularVelocity;
        Destroy(gameObject, 3f);
    }
}
