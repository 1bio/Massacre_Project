using UnityEngine;
using UnityEngine.InputSystem;
using System;

[Serializable]
public class InputReader : MonoBehaviour,InputActions.IPlayerActions
{
    public InputActions InputActions { get; private set; }

    public Vector2 MoveValue { get; private set; }

    public bool IsAttacking { get; private set; }

    public bool IsAiming { get; private set; }

    public bool isAutoRotate { get; private set; } 


    public event Action RollEvent;


    private void Awake()
    {
        InputActions = new InputActions();

        InputActions.Player.SetCallbacks(this);

        InputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        InputActions.Player.Disable();
    }



    #region Input Method
    // Moving
    void InputActions.IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        MoveValue = context.ReadValue<Vector2>();
    }

    // Attacking
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if(context.canceled)
        {
            IsAttacking = false;
        }
    }

    // Rotating
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAiming = true;
            isAutoRotate = false;
        }
        else if (context.canceled)
        {
            IsAiming = false;
            isAutoRotate = true;
        }
    }

    // Rolling
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        RollEvent?.Invoke();
    }
    #endregion
}
