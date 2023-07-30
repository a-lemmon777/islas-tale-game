using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Responsible for all combat-related code for the Mermaid.
/// </summary>
[RequireComponent(typeof(MermaidInput))]
public class MermaidCombat : MonoBehaviour
{
    [Tooltip("Time between starfish throws in seconds")]
    public float StarfishCooldown = 0.32f;

    [Tooltip("The distance from center from which to shoot projectiles")]
    public float ProjectileReleaseOffset = 1.15f;

    [Tooltip("Starfish Prefab")]
    public GameObject StarfishPrefab;

    [Tooltip("Reference to the throw sound effect")]
    public AudioSource ThrowSound;

    private float _nextStarfishThrowTime = 0f;
    private Vector2 _attackDirection = Vector2.zero;
    private bool _rangedAttackInputActive = false;
    private bool _rangedAttackQueued = false;
    private float _lastHorizontalInput = 1f; // Default to right

    /// <summary>
    /// Reference to the mermaid input script
    /// </summary>
    private MermaidInput _mermaidInput;

    /// <summary>
    /// Reference to the mermaid animator script
    /// </summary>
    private MermaidAnimator _mermaidAnimator;

    private void Awake()
    {
        _mermaidInput = GetComponent<MermaidInput>();
        _mermaidAnimator = GetComponentInChildren<MermaidAnimator>();
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
        _attackDirection = direction;
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

    private void Update()
    {
        if (Time.time >= _nextStarfishThrowTime && (_rangedAttackInputActive || _rangedAttackQueued))
        {
            _mermaidAnimator.HandleAttack();
            _nextStarfishThrowTime = Time.time + StarfishCooldown;
        }

    }

    public void ThrowStarfish()
    {
        ThrowStarfish(_attackDirection);
    }

    /// <summary>
    /// Attack by throwing a starfish. If the player has stopped aiming, the mermaid will
    /// continue to throw in the last left/right direction that was input.
    /// </summary>
    /// <param name="normalizedDirection">The direction to throw the starfish.</param>
    public void ThrowStarfish(Vector2 normalizedDirection)
    {
        ThrowSound.Play();
        _rangedAttackQueued = false;
        Vector3 throwReleaseOffset = (Vector3)(normalizedDirection * ProjectileReleaseOffset * transform.localScale.x);
        Vector3 spawnLocation = transform.position + throwReleaseOffset;
        if (normalizedDirection == Vector2.zero)
            normalizedDirection.x = _lastHorizontalInput;
        GameObject starfish = Instantiate(StarfishPrefab, spawnLocation, Quaternion.identity);
        starfish.GetComponent<StarfishController>().SetDirection(normalizedDirection);
    }
}
