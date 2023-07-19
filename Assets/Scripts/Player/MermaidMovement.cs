using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MermaidMovement : MonoBehaviour
{
    [Tooltip("Speed in units per second")]
    public float Speed = 10f;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Makes the player move at modified maximum speed along the border
    /// </summary>
    /// <returns>Adjusted movement delta</returns>
    public Vector2 Move(Vector2 input)
    {
        float halfColliderWidth = _collider.bounds.extents.x;
        float halfColliderHeight = _collider.bounds.extents.y;

        float lowerBoundX = BoundaryController.SCREEN_MIN_X + halfColliderWidth;
        float upperBoundX = BoundaryController.SCREEN_MAX_X - halfColliderWidth;
        float lowerBoundY = BoundaryController.SCREEN_MIN_Y + halfColliderHeight;
        float upperBoundY = BoundaryController.SCREEN_MAX_Y - halfColliderHeight;

        float remainingDistanceX = input.x > 0 ? upperBoundX - transform.position.x : lowerBoundX - transform.position.x;
        float remainingDistanceY = input.y > 0 ? upperBoundY - transform.position.y : lowerBoundY - transform.position.y;

        Vector2 movement = Vector2.zero;
        float movementMagnitude = Speed * Time.deltaTime;

        // Used for Pythagorean Theorem
        float movementMagnitudeSquared = Mathf.Pow(movementMagnitude, 2);

        // The case when the player inputs diagonal movement.
        if (Mathf.Abs(input.x) > 0 && Mathf.Abs(input.y) > 0)
        {
            // The magnitude of movement per axis if the full movement is split equally between both dimensions.
            float perAxisEqualMovement = Mathf.Sqrt(movementMagnitudeSquared / 2);

            // The case when the player is close to the left or right edge.
            if (Mathf.Abs(remainingDistanceX) < perAxisEqualMovement)
            {
                movement.x = remainingDistanceX;
                float availableYMagnitude = Mathf.Sqrt(movementMagnitudeSquared - Mathf.Pow(movement.x, 2));
                float movementY = Mathf.Clamp(remainingDistanceY, -availableYMagnitude, availableYMagnitude);
                movement.y = movementY;
            }
            // The case when the player is close to the upper or lower edge.
            else if (Mathf.Abs(remainingDistanceY) < perAxisEqualMovement)
            {
                movement.y = remainingDistanceY;
                float availableXMagnitude = Mathf.Sqrt(movementMagnitudeSquared - Mathf.Pow(movement.y, 2));
                float movementX = Mathf.Clamp(remainingDistanceX, -availableXMagnitude, availableXMagnitude);
                movement.x = movementX;
            }
            // The case when the player is not close to any edge.
            else
            {
                movement.x = input.x > 0 ? perAxisEqualMovement : -perAxisEqualMovement;
                movement.y = input.y > 0 ? perAxisEqualMovement : -perAxisEqualMovement;
            }
        }
        // The case when the player inputs only horizontal movement.
        else if (Mathf.Abs(input.x) > 0)
        {
            movement.x = Mathf.Clamp(remainingDistanceX, -movementMagnitude, movementMagnitude);
        }
        // The case when the player inputs only vertical movement.
        else if (Mathf.Abs(input.y) > 0)
        {
            movement.y = Mathf.Clamp(remainingDistanceY, -movementMagnitude, movementMagnitude);
        }

        Vector2 position = _rigidbody2D.position + movement;

        _rigidbody2D.MovePosition(position);

        return movement;
    }
}
