using UnityEngine;
using UnityEngine.Events;

public class MermaidInput : MonoBehaviour
{
    [Tooltip("Reference to the InputReader scriptable object")]
    public InputReader _inputReader;

    public event UnityAction<Vector2> MermaidMoveEvent = delegate { };
    public event UnityAction MermaidRangedAttackEvent = delegate { };
    public event UnityAction MermaidRangedAttackCanceledEvent = delegate { };
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
        MermaidMoveEvent.Invoke(direction);
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
        MermaidAimEvent.Invoke(direction);
    }

    // Translate the location to a direction from the player character.
    private void OnAimLocation(Vector2 location)
    {
        Vector2 locationWorldCoords = (Vector2) Camera.main.ScreenToWorldPoint(location);
        Vector2 direction = locationWorldCoords - (Vector2) transform.position;
        MermaidAimEvent.Invoke(direction);
    }

}
