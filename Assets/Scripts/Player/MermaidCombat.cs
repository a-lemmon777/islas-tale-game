using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MermaidInput))]
public class MermaidCombat : MonoBehaviour
{
    [Tooltip("Time between starfish throws in seconds")]
    public float StarfishCooldown = 0.2f;

    [Tooltip("The distance from center from which to throw projectiles")]
    public float ThrowReleaseOffset = 2f;

    [Tooltip("Starfish Prefab")]
    public GameObject StarfishPrefab;

    private float _nextStarfishThrowTime = 0f;
    private Vector2 _throwDirection = Vector2.zero;
    private bool _rangedAttackInputActive = false;
    private bool _rangedAttackQueued = false;
    private float _lastHorizontalInput = 1f; // Default to right

    /// <summary>
    /// Reference to the mermaid input script
    /// </summary>
    private MermaidInput _mermaidInput;

    private void Awake()
    {
        _mermaidInput = GetComponent<MermaidInput>();
    }

    // This function is called when the object becomes enabled and active
    private void OnEnable()
    {
        _mermaidInput.MermaidAimEvent += OnAim;
        _mermaidInput.MermaidRangedAttackEvent += OnRangedAttack;
        _mermaidInput.MermaidRangedAttackCanceledEvent += OnRangedAttackCanceled;
    }

    // This function is called when the behaviour becomes disabled or inactive
    private void OnDisable()
    {
        _mermaidInput.MermaidAimEvent -= OnAim;
        _mermaidInput.MermaidRangedAttackEvent -= OnRangedAttack;
        _mermaidInput.MermaidRangedAttackCanceledEvent -= OnRangedAttackCanceled;
    }

    private void OnAim(Vector2 direction)
    {
        if (direction.x != 0)
            _lastHorizontalInput = direction.x;
        _throwDirection = direction;
    }

    private void OnRangedAttack()
    {
        _rangedAttackQueued = true;
        _rangedAttackInputActive = true;
    }

    private void OnRangedAttackCanceled()
    {
        _rangedAttackInputActive = false;
    }

    private void FixedUpdate()
    {
        ThrowStarfish();
    }

    /// <summary>
    /// Attack by throwing a starfish. If the player has stopped aiming, the mermaid will
    /// continue to throw in the last left/right direction that was input.
    /// </summary>
    /// <param name="direction">The direction to throw the starfish.</param>
    public void ThrowStarfish()
    {
        float currentTime = Time.time;
        if (currentTime >= _nextStarfishThrowTime && (_rangedAttackInputActive || _rangedAttackQueued))
        {
            _rangedAttackQueued = false;
            _nextStarfishThrowTime = currentTime + StarfishCooldown;
            Vector3 throwReleaseOffset = (Vector3)(_throwDirection.normalized * ThrowReleaseOffset * transform.localScale.x);
            Vector3 spawnLocation = transform.position + throwReleaseOffset;
            GameObject starfish = Instantiate(StarfishPrefab, spawnLocation, Quaternion.identity);
            Vector2 throwDirection = _throwDirection;
            if (throwDirection == Vector2.zero)
                throwDirection.x = _lastHorizontalInput;
            starfish.GetComponent<StarfishController>().SetDirection(throwDirection.normalized);
        }
    }
}
