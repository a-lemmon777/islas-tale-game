using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    /// <summary>
    /// The animator state machine for the player
    /// </summary>
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void HandleMovement(Vector2 normalizedInput)
    {
        _animator.SetFloat("Horizontal Direction", normalizedInput.x);
    }
}
