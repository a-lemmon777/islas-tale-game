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
    /// Sets the float variable for the source of the damage taken
    /// </summary>
    /// <param name="damageSource">Negative: hit from the left, Positive: hit from the right</param>
    public void HandleDamage(float damageSource)
    {
        _animator.SetTrigger("Damage");
    }

    /// <summary>
    /// Initiate the dying animation
    /// </summary>
    public void HandleDeath()
    {
        _animator.ResetTrigger("Damage");
        _animator.SetTrigger("Die");
        _animator.SetBool("Alive", false);
    }


    /// <summary>
    /// Turns the shrimp into powered mode
    /// </summary>
    public void HandlePower()
    {
        _animator.SetTrigger("Power Up");
        _animator.SetBool("Alive", true);
    }
}
