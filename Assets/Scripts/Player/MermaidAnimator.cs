using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MermaidAnimator : MonoBehaviour
{
    /// <summary>
    /// The animator state machine for the player
    /// </summary>
    private Animator _animator;
    private MermaidInput _mermaidInput;
    private MermaidCombat _mermaidCombat;

    [Tooltip("Scriptable object for level events")]
    public LevelEvents LevelEvents;

    private Vector2 _moveDirection = Vector2.zero;
    private float _idleDirection = 0f; // The direction to face when idling

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _mermaidInput = GetComponentInParent<MermaidInput>();
        _mermaidCombat = GetComponentInParent<MermaidCombat>();
    }

    private void OnEnable()
    {
        _mermaidInput.MermaidAimEvent += OnAim;
        _mermaidInput.MermaidMoveEvent += OnMove;
    }

    private void OnDisable()
    {
        _mermaidInput.MermaidAimEvent -= OnAim;
        _mermaidInput.MermaidMoveEvent -= OnMove;
    }

    private void OnAim(Vector2 direction)
    {
        _idleDirection = direction.x;
    }

    private void OnMove(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void Update()
    {
        _animator.SetBool("Is Swimming", _moveDirection != Vector2.zero);
        _animator.SetFloat("Horizontal Velocity", _moveDirection.x);
        _animator.SetFloat("Idle Direction", _idleDirection);
    }

    public void HandleAttack(Vector2 normalizedInput)
    {
        _animator.SetFloat("Aim X", normalizedInput.x);
        _animator.SetFloat("Aim Y", normalizedInput.y);
        _animator.SetTrigger("Attack");
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
        _animator.SetTrigger("Hurt");
    }

    /// <summary>
    /// Triggers the dying animations. The mermaid stays dead visually.
    /// </summary>
    public void HandleDeath()
    {
        _animator.SetBool("Is Dying", true);
    }

    // Handlers called by Animation Clips
    public void ThrowStarfishUp()
    {
        _mermaidCombat.ThrowStarfish(Vector2.up);
    }

    public void ThrowStarfishUpRight()
    {
        _mermaidCombat.ThrowStarfish(new Vector2(1, 1));
    }

    public void ThrowStarfishRight()
    {
        _mermaidCombat.ThrowStarfish(Vector2.right);
    }

    public void ThrowStarfishDownRight()
    {
        _mermaidCombat.ThrowStarfish(new Vector2(1, -1));
    }

    public void ThrowStarfishDown()
    {
        _mermaidCombat.ThrowStarfish(Vector2.down);
    }

    public void ThrowStarfishDownLeft()
    {
        _mermaidCombat.ThrowStarfish(new Vector2(-1, -1));
    }

    public void ThrowStarfishLeft()
    {
        _mermaidCombat.ThrowStarfish(Vector2.left);
    }

    public void ThrowStarfishUpLeft()
    {
        _mermaidCombat.ThrowStarfish(new Vector2(-1, 1));
    }
}
