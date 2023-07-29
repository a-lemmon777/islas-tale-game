using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ShrimpAnimator))]
public class ShrimpHealth : CharacterHealth
{
    [Tooltip("Reference to the shrimp prefab root")]
    public GameObject PrefabRoot;

    public UnityEvent PowerUp;

    [Tooltip("Reference to the wave manager of this enemy")]
    private WaveManager _partOfWave;

    private ShrimpAnimator _shrimpAnimator;

    private void Awake()
    {
        PowerUp.AddListener(() =>
        {
            Heal(MaxHealth);
            _shrimpAnimator.HandlePower();
        });
    }

    private void Start()
    {
        _shrimpAnimator = GetComponent<ShrimpAnimator>();
        _partOfWave = GetComponentInParent<WaveManager>();
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

    private void OnDestroy()
    {
        _partOfWave.EnemyDown.Invoke();
    }
}
