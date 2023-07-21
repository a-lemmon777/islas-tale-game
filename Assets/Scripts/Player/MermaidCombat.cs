using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Aim"].performed += context => OnAimEvent(context);
        _playerInput.actions["Aim"].canceled += context => OnAimEvent(context);
        _playerInput.actions["RangedAttack"].performed += _ => OnRangedAttackPerformed();
        _playerInput.actions["RangedAttack"].canceled += _ => OnRangedAttackCanceled();
    }

    private void OnAimEvent(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        if (direction.x != 0)
            _lastHorizontalInput = direction.x;
        _throwDirection = direction;
    }

    private void OnRangedAttackPerformed()
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
    /// Attack by throwing a starfish.
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
