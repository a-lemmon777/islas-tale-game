using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Tooltip("Destination position to move to")]
    public Transform Destination;

    [Tooltip("Speed in units per second")]
    public float Speed;

    [Tooltip("How long this obstacle lives before being destroyed")]
    public float LifeTime;

    private Rigidbody2D _rigidbody2D;

    private void OnEnable()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();

        _rigidbody2D.velocity = ((Vector2)Destination.position - _rigidbody2D.position).normalized * Speed;

        Destroy(gameObject, LifeTime);
    }
}
