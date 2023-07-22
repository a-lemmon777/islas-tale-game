using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Reflection;

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

    /* Menus Event Handlers */

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed)
            MenuConfirmEvent.Invoke();
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
