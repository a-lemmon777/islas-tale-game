using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(MermaidHealth))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Reference to the animation controller script of the player")]
    public MermaidAnimator AnimationController;

    /// <summary>
    /// Reference to the mermaid health script
    /// </summary>
    private MermaidHealth _mermaidHealth;

    [Tooltip("Speed in units per second")]
    public float Speed = 10f;

    [Tooltip("Starfish Prefab")]
    public GameObject StarfishPrefab;

    [Tooltip("Time between starfish throws in seconds")]
    public float StarfishCooldown = 0.2f;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider;
    private float _horizontalInput = 0f;
    private float _verticalInput = 0f;
    private float _nextStarfishThrowTime = 0f;


    /// <summary>
    /// Test for the hurt animation
    /// </summary>
    private float _lastHorizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _mermaidHealth = GetComponent<MermaidHealth>();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextStarfishThrowTime)
        {
            ThrowStarfish();
        }

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        var movement = MoveAlongBorders();
        Vector2 position = _rigidbody2D.position + movement;

        _rigidbody2D.MovePosition(position);

        // animation triggers
        this.AnimationController.HandleMovement(movement);
        // test
        if (_horizontalInput != 0)
            _lastHorizontalInput = _horizontalInput;
    }

    void Update()
    {
        // test
        if (Input.GetKeyDown(KeyCode.H))
        {
            this.AnimationController.HandleDamage(_lastHorizontalInput);
            this._mermaidHealth.TakeDamage(1);
        }
    }

    /// <summary>
    /// Makes the player move at modified maximum speed along the border
    /// </summary>
    /// <returns>Adjusted movement delta</returns>
    private Vector2 MoveAlongBorders()
    {
        float halfColliderWidth = _collider.bounds.extents.x;
        float halfColliderHeight = _collider.bounds.extents.y;

        float lowerBoundX = BoundaryController.SCREEN_MIN_X + halfColliderWidth;
        float upperBoundX = BoundaryController.SCREEN_MAX_X - halfColliderWidth;
        float lowerBoundY = BoundaryController.SCREEN_MIN_Y + halfColliderHeight;
        float upperBoundY = BoundaryController.SCREEN_MAX_Y - halfColliderHeight;

        float remainingDistanceX = _horizontalInput > 0 ? upperBoundX - transform.position.x : lowerBoundX - transform.position.x;
        float remainingDistanceY = _verticalInput > 0 ? upperBoundY - transform.position.y : lowerBoundY - transform.position.y;

        Vector2 movement = Vector2.zero;
        float movementMagnitude = Speed * Time.deltaTime;

        // Used for Pythagorean Theorem
        float movementMagnitudeSquared = Mathf.Pow(movementMagnitude, 2);

        // The case when the player inputs diagonal movement.
        if (Mathf.Abs(_horizontalInput) > 0 && Mathf.Abs(_verticalInput) > 0)
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
                movement.x = _horizontalInput > 0 ? perAxisEqualMovement : -perAxisEqualMovement;
                movement.y = _verticalInput > 0 ? perAxisEqualMovement : -perAxisEqualMovement;
            }
        }
        // The case when the player inputs only horizontal movement.
        else if (Mathf.Abs(_horizontalInput) > 0)
        {
            movement.x = Mathf.Clamp(remainingDistanceX, -movementMagnitude, movementMagnitude);
        }
        // The case when the player inputs only vertical movement.
        else if (Mathf.Abs(_verticalInput) > 0)
        {
            movement.y = Mathf.Clamp(remainingDistanceY, -movementMagnitude, movementMagnitude);
        }

        return movement;
    }

    void ThrowStarfish()
    {
        _nextStarfishThrowTime = Time.time + StarfishCooldown;
        GameObject starfish = Instantiate(StarfishPrefab, transform.position, transform.rotation);
        starfish.GetComponent<StarfishController>().SetDirection(new Vector2(1, 1));
    }
}
