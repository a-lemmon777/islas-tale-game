using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BoundaryController : MonoBehaviour
{

    private Collider2D _collider;
    public const int SCREEN_WIDTH = 32;
    public const int SCREEN_HEIGHT = 18;
    public const int SCREEN_MIN_X = -(SCREEN_WIDTH / 2);
    public const int SCREEN_MAX_X = SCREEN_WIDTH / 2;
    public const int SCREEN_MIN_Y = -(SCREEN_HEIGHT / 2);
    public const int SCREEN_MAX_Y = SCREEN_HEIGHT / 2;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// LateUpdate is called after FixedUpdate(), all physics calculations, and Update().
    /// If any GameObject has this script attached, if it happens to accidentally move off the screen
    /// due to physics or anything else, it will be brought back in bounds before being rendered to the screen.
    /// </summary>
    void LateUpdate()
    {
        float halfColliderWidth = _collider.bounds.extents.x;
        float halfColliderHeight = _collider.bounds.extents.y;
        Vector2 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, SCREEN_MIN_X + halfColliderWidth, SCREEN_MAX_X - halfColliderWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, SCREEN_MIN_Y + halfColliderHeight, SCREEN_MAX_Y - halfColliderHeight);
        transform.position = clampedPosition;
    }
}
