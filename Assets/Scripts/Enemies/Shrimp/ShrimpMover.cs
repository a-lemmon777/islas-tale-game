using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShrimpMover : MonoBehaviour
{
    [Tooltip("Destination position to move to")]
    public Transform Destination;

    [Tooltip("By default, the sprite looks towards +x. This vector will rotate the sprite to face toward the destination.")]
    public Vector2 FacingTowards;

    [Tooltip("Speed in units per second")]
    public float Speed = 5;

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();

        // Rotate the image according to the facing vector
        this.FacingTowards = (Vector2)Destination.position - _rigidbody2D.position;
        this.FacingTowards.Normalize();
        this.transform.Rotate(Vector3.forward * Vector2.Angle(Vector2.right, this.FacingTowards));
    }

    // Update is called once per frame
    void Update()
    {
        this._rigidbody2D.MovePosition(
            Vector3.MoveTowards(
                _rigidbody2D.position,
                Destination.position,
                Speed * Time.deltaTime
            )
        );
    }
}
