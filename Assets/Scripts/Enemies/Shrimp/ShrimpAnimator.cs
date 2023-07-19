using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShrimpAnimator : MonoBehaviour
{
    /// <summary>
    /// Reference to the animator component of the shrimp
    /// </summary>
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Sets the float variable for the orientation of the shrimp
    /// </summary>
    /// <param name="horizontalOrientation">Negative: facing left, Positive: facing right</param>
    public void HandleMovement(float horizontalOrientation)
    {
        // _animator.SetFloat("Horizontal Orientation", horizontalOrientation);
    }

    /// <summary>
    /// Sets the float variable for the source of the damage taken
    /// </summary>
    /// <param name="damageSource">Negative: hit from the left, Positive: hit from the right</param>
    public void HandleDamage(float damageSource)
    {
        // _animator.SetFloat("Horizontal Damage Source", damageSource);
        _animator.SetTrigger("Damage");
    }

    /// <summary>
    /// Initiate the dying animation
    /// </summary>
    public void HandleDeath()
    {
        _animator.ResetTrigger("Damage");
        _animator.SetTrigger("Die");
    }
}
