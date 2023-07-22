using UnityEngine;
using UnityEngine.Events;

public class MermaidInput : MonoBehaviour
{
    [Tooltip("Reference to the InputReader scriptable object")]
    public InputReader _inputReader;

    /// <summary>
    /// Provides a normalized, 8-directional vector as the move direction.
    /// </summary>
    public event UnityAction<Vector2> MermaidMoveEvent = delegate { };
    public event UnityAction MermaidRangedAttackEvent = delegate { };
    public event UnityAction MermaidRangedAttackCanceledEvent = delegate { };
    /// <summary>
    /// Provides a normalized, 8-directional vector as the aim direction.
    /// </summary>
    public event UnityAction<Vector2> MermaidAimEvent = delegate { };

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.RangedAttackEvent += OnRangedAttack;
        _inputReader.RangedAttackCanceledEvent += OnRangedAttackCancel;
        _inputReader.AimDirectionEvent += OnAimDirection;
        _inputReader.AimLocationEvent += OnAimLocation;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.RangedAttackEvent -= OnRangedAttack;
        _inputReader.RangedAttackCanceledEvent -= OnRangedAttackCancel;
        _inputReader.AimDirectionEvent -= OnAimDirection;
        _inputReader.AimLocationEvent -= OnAimLocation;
    }

    private void OnMove(Vector2 direction)
    {
        MermaidMoveEvent.Invoke(ConvertToNormalizedEightDirectionalVector(direction));
    }

    private void OnRangedAttack()
    {
        MermaidRangedAttackEvent.Invoke();
    }

    private void OnRangedAttackCancel()
    {
        MermaidRangedAttackCanceledEvent.Invoke();
    }

    private void OnAimDirection(Vector2 direction)
    {
        MermaidAimEvent.Invoke(ConvertToNormalizedEightDirectionalVector(direction));
    }

    // Translate the location to a direction from the player character.
    private void OnAimLocation(Vector2 location)
    {
        Vector2 locationWorldCoords = (Vector2) Camera.main.ScreenToWorldPoint(location);
        Vector2 direction = locationWorldCoords - (Vector2) transform.position;
        MermaidAimEvent.Invoke(ConvertToNormalizedEightDirectionalVector(direction));
    }

    private Vector2 ConvertToNormalizedEightDirectionalVector(Vector2 vector)
    {
        if (vector == Vector2.zero)
            return vector;

        float angleInRadians = Mathf.Atan2(vector.y, vector.x);
        float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
        // Round the angle to a multiple of 45, which allows for only 8 directions.
        float angleLockedToEightDirections = Mathf.Round(angleInDegrees / 45.0f) * 45.0f;

        // These values should now be very close to either 0, 0.71 (diagonal), or 1.
        float xComponent = Mathf.Cos(angleLockedToEightDirections * Mathf.Deg2Rad);
        float yComponent = Mathf.Sin(angleLockedToEightDirections * Mathf.Deg2Rad);

        // Round the components. If movement was diagonal, both components will round to 1.
        xComponent = Mathf.Round(xComponent);
        yComponent = Mathf.Round(yComponent);

        Vector2 normalizedVector = new Vector2(xComponent, yComponent).normalized;
        return normalizedVector;
    }

}
