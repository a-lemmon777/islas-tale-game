using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveRight : MonoBehaviour
{
    [Tooltip("Speed in units per second")]
    public float Speed;

    /// <summary>
    /// Rigidbody without gravity
    /// </summary>
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
            (Vector2)this.transform.position
            + Vector2.right * this.Speed * Time.deltaTime
        );
    }
}
