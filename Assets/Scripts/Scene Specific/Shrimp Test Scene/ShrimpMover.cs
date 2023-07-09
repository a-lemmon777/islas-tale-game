using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShrimpMover : MonoBehaviour
{
    [Tooltip("Destination position to move to")]
    public Transform Destination;

    /// <summary>
    /// Moving speed in units per second
    /// </summary>
    public float Speed = 1;

    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this._rigidbody2D.MovePosition(
            Vector3.MoveTowards(
                this._rigidbody2D.position,
                this.Destination.position,
                this.Speed * Time.deltaTime
            )
        );
    }
}
