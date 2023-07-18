using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShrimpAnimator))]
public class ShrimpMover : MonoBehaviour
{
    [Tooltip("Destination position to move to")]
    public Transform Destination;

    [Tooltip("Speed in units per second")]
    public float Speed;

    /// <summary>
    /// Shrimp animator
    /// </summary>
    private ShrimpAnimator _shrimpAnimator;

    private void Start()
    {
        _shrimpAnimator = GetComponent<ShrimpAnimator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceLeft = Vector2.Distance(Destination.position, transform.position);

        // the shrimp arrived
        if (distanceLeft <= 0.05f) // proximity threshold, 0.05f makes the shrimp stop on the target
        {
            return;
        }

        var nextPosition = Vector2.MoveTowards(
            transform.position,
            Destination.position,
            Speed * Time.deltaTime
        );

        var horizontalDirection = (nextPosition - (Vector2)transform.position).x;
        transform.position = nextPosition;

        // animation triggers
        _shrimpAnimator.HandleMovement(horizontalDirection);
    }
}
