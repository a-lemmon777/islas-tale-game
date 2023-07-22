using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MermaidHealth))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("Reference to the animation controller script of the player")]
    public MermaidAnimator AnimationController;

    /// <summary>
    /// Reference to the mermaid health script
    /// </summary>
    private MermaidHealth _mermaidHealth;

    private float _horizontalInput = 0f;

    /// <summary>
    /// Test for the hurt animation
    /// </summary>
    private float _lastHorizontalInput;

    void Awake()
    {
        _mermaidHealth = GetComponent<MermaidHealth>();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {

        _horizontalInput = Input.GetAxisRaw("Horizontal");

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
