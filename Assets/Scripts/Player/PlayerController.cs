using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MermaidHealth))]
[RequireComponent(typeof(MermaidMovement))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Reference to the animation controller script of the player")]
    public MermaidAnimator AnimationController;

    /// <summary>
    /// Reference to the mermaid health script
    /// </summary>
    private MermaidHealth _mermaidHealth;

    /// <summary>
    /// Reference to the mermaid movement script
    /// </summary>
    private MermaidMovement _mermaidMovement;

    private float _horizontalInput = 0f;
    private float _verticalInput = 0f;

    /// <summary>
    /// Test for the hurt animation
    /// </summary>
    private float _lastHorizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        _mermaidHealth = GetComponent<MermaidHealth>();
        _mermaidMovement = GetComponent<MermaidMovement>();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        var movement = _mermaidMovement.Move(new Vector2(_horizontalInput, _verticalInput));

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
}
