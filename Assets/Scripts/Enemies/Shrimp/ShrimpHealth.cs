using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ShrimpAnimator))]
public class ShrimpHealth : CharacterHealth
{
    [Tooltip("Reference to the shrimp prefab root")]
    public GameObject PrefabRoot;

    [HideInInspector]
    public UnityEvent PowerUp;

    [Tooltip("Reference to the wave manager of this enemy")]
    private WaveManager _partOfWave;

    private ShrimpAnimator _shrimpAnimator;

    [Tooltip("Reference to the hurt sound effect")]
    public AudioSource HurtSound;

    [Tooltip("Reference to the die sound effect")]
    public AudioSource DieSound;

    [Tooltip("Reference to the power up sound effect")]
    public AudioSource PowerUpSound;

    [Tooltip("Reference to the shrimp mover")]
    public ShrimpMover Mover;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        this._rigidbody2D = GetComponent<Rigidbody2D>();
        PowerUp.AddListener(() =>
        {
            PowerUpSound.Play();
            Heal(MaxHealth);
            _shrimpAnimator.HandlePower();
        });
    }

    private void OnEnable()
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
        // Only play hurt sound if shrimp will not die.
        if (this.Health > damageValue)
        {
            HurtSound.Play();
        }


        base.TakeDamage(damageValue);
        _shrimpAnimator.HandleDamage(damageSource);


    }

    /// <summary>
    /// Triggers the appropriate callbacks for dying
    /// </summary>
    public override void Die()
    {
        Mover.IsDead = true;
        // Slow down a lot, but keep current orientation.
        _rigidbody2D.velocity *= 0.01f;
        DieSound.Play();
        // Turn off the collider so it doesn't soak extra hits and play extra sounds.
        GetComponentInChildren<Collider2D>().enabled = false;
        _shrimpAnimator.HandleDeath();
    }

    private void OnDestroy()
    {
        _partOfWave.EnemyDown.Invoke();
    }
}
