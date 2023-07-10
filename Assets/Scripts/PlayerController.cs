using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacterScript : MonoBehaviour
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

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 horizontalInputVector = new Vector2(horizontalInput, 0).normalized;
        Vector2 verticalInputVector = new Vector2(0, verticalInput).normalized;
        Vector2 direction = (horizontalInputVector + verticalInputVector).normalized;

        Vector2 movement = direction * Speed * Time.deltaTime;
        Vector2 position = (Vector2) this.transform.position;
        position += movement;

        _rigidbody2D.MovePosition(position);
    }
}
