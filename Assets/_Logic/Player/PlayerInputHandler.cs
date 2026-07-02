using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveInput;
    public event Action<Vector2> OnLookInput;
    public event Action OnJumpInput;
    public event Action OnShootInput;
    public event Action OnReloadInput;

    private PlayerInputActions actions;

    private void Awake()
    {
        actions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        if(actions != null)
        {
            actions.Enable();

            actions.Game.Move.performed += OnMoveCall;
            actions.Game.Move.canceled += OnMoveCall;

            actions.Game.Look.performed += OnLookCall;
            actions.Game.Look.canceled += OnLookCall;

            actions.Game.Jump.performed += OnJumpCall;

            actions.Game.Shoot.performed += OnShootCall;

            actions.Game.Reload.performed += OnReloadCall;
        }
    }

    private void OnDisable()
    {
        if(actions != null)
        {
            actions.Game.Move.performed -= OnMoveCall;
            actions.Game.Move.canceled -= OnMoveCall;

            actions.Game.Look.performed -= OnLookCall;
            actions.Game.Look.canceled -= OnLookCall;

            actions.Game.Jump.performed -= OnJumpCall;

            actions.Game.Shoot.performed -= OnShootCall;

            actions.Game.Reload.performed -= OnReloadCall;

            actions.Disable();
        }
    }

    private void OnDestroy()
    {
        if(actions != null)
        {
            actions.Dispose();
        }
    }

    private void OnMoveCall(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        OnMoveInput?.Invoke(input);
    }

    private void OnLookCall(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        OnLookInput?.Invoke(input);
    }

    private void OnJumpCall(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            OnJumpInput?.Invoke();
        }
    }

    private void OnShootCall(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            OnShootInput?.Invoke();
        }
    }

    private void OnReloadCall(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            OnReloadInput?.Invoke();
        }
    }
}
