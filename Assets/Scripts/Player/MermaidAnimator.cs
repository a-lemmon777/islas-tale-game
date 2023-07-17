using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MermaidAnimator : MonoBehaviour
{
    /// <summary>
    /// The animator state machine for the player
    /// </summary>
    private Animator _animator;

    [Tooltip("How long it takes to start the neutral idling in seconds")]
    public float TimeToIdle = 2;
    private float _nextIdleTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Triggers the animation parameters for swimming and idle animations.
    /// The parameter is "Horizontal Velocity" which represents the velocity of the mermaid
    /// received through player input.
    /// </summary>
    /// <param name="normalizedInput">An instantaneous frame movement delta</param>
    public void HandleMovement(Vector2 normalizedInput)
    {
        _animator.SetFloat("Horizontal Velocity", normalizedInput.x);

        // transition to idle
        if (normalizedInput != Vector2.zero)
        {
            _nextIdleTime = Time.time + TimeToIdle;
        }

        if (Time.time > _nextIdleTime)
        {
            _animator.SetTrigger("Idle");
            return;
        }

        _animator.ResetTrigger("Idle");
    }


    /// <summary>
    /// Triggers the animation parameters for damage taken animations.
    /// The parameter is "Damage Source Horizontal" which represents the location where the
    /// mermaid gets hurt relative to her center of mass.
    /// </summary>
    /// <param name="damageSourceHorizontal">Negative: hit from the left. Positive:
    /// hit from the right. Magnitude is ignored. </param>
    public void HandleDamage(float damageSourceHorizontal)
    {
        _animator.SetFloat("Damage Source Horizontal", damageSourceHorizontal);
        _animator.SetTrigger("Damage");
    }

    /// <summary>
    /// Triggers the dying animations. The mermaid stays dead visually.
    /// </summary>
    public void HandleDeath()
    {
        _animator.ResetTrigger("Damage");
        _animator.SetTrigger("Die");
    }
}
