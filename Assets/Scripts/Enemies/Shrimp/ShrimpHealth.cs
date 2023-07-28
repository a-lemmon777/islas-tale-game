using UnityEngine;

[RequireComponent(typeof(ShrimpAnimator))]
public class ShrimpHealth : CharacterHealth
{
    [Tooltip("Reference to the shrimp prefab root")]
    public GameObject PrefabRoot;

    private ShrimpAnimator _shrimpAnimator;

    private void Start()
    {
        _shrimpAnimator = GetComponent<ShrimpAnimator>();
    }

    /// <summary>
    /// Triggers the appropriate callbacks for taking damage 
    /// </summary>
    /// <param name="damageValue">How much damage to take</param>
    /// <param name="damageSource">Source of the damage in x-axis. Negative is left.</param>
    public void TakeDamage(int damageValue, float damageSource)
    {
        if (damageValue < 0) Debug.LogWarning("Negative damage received!");

        base.TakeDamage(damageValue);

        _shrimpAnimator.HandleDamage(damageSource);
    }

    /// <summary>
    /// Triggers the appropriate callbacks for dying
    /// </summary>
    public override void Die()
    {
        _shrimpAnimator.HandleDeath();
    }
}
