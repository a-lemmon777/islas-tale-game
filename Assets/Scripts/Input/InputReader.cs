using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Reflection;

/// <summary>
/// Exposes all inputs defined in the PlayerControls.inputactions asset as UnityActions that
/// can be subscribed to. This class automatically updates the InputReader scriptable object
/// asset, which can be attached to nearly anything in the Unity Inspector. When new input actions
/// are defined, this file must be updated to handle the new events and expose them via new UnityActions.
/// </summary>
[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerControls.IGameplayActions, PlayerControls.IMenusActions
{
    // Gameplay
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction RangedAttackEvent = delegate { };
    public event UnityAction RangedAttackCanceledEvent = delegate { };
    public event UnityAction<Vector2> AimDirectionEvent = delegate { };
    public event UnityAction<Vector2> AimLocationEvent = delegate { };

    // Menus
    public event UnityAction MenuConfirmEvent = delegate { };

    private PlayerControls _playerControls;

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            _playerControls.Gameplay.SetCallbacks(this);
            _playerControls.Menus.SetCallbacks(this);
        }

        EnableGameplayInput();

        // level states
        LevelEvents.Instance.Pause.AddListener(EnableMenuInput);
        LevelEvents.Instance.Resume.AddListener(EnableGameplayInput);
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    /* Gameplay Event Handlers */

    void PlayerControls.IGameplayActions.OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
            MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    void PlayerControls.IGameplayActions.OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RangedAttackEvent.Invoke();
        }

        if (context.canceled)
        {
            RangedAttackCanceledEvent.Invoke();
        }
    }

    void PlayerControls.IGameplayActions.OnAimDirection(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
            AimDirectionEvent.Invoke(context.ReadValue<Vector2>());
    }

    void PlayerControls.IGameplayActions.OnAimLocation(InputAction.CallbackContext context)
    {
        if (context.performed)
            AimLocationEvent.Invoke(context.ReadValue<Vector2>());
    }

    void PlayerControls.IGameplayActions.OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            LevelEvents.Instance.Pause.Invoke();
    }

    /* Menus Event Handlers */

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed)
            MenuConfirmEvent.Invoke();
    }

    void PlayerControls.IMenusActions.OnResume(InputAction.CallbackContext context)
    {
        if (context.performed)
            LevelEvents.Instance.Resume.Invoke();
    }

    /* Enabling and Disabling */

    public void EnableGameplayInput()
    {
        _playerControls.Menus.Disable();

        _playerControls.Gameplay.Enable();
    }

    public void EnableMenuInput()
    {
        _playerControls.Gameplay.Disable();

        _playerControls.Menus.Enable();
    }

    public void DisableAllInput()
    {
        _playerControls.Gameplay.Disable();
        _playerControls.Menus.Disable();
    }
}
