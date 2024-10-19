using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.Interactions;

[Serializable]
public class InputReader : MonoBehaviour, InputActions.IPlayerActions
{
    public InputActions InputActions { get; private set; }

    public Vector2 MoveValue { get; private set; }

    public bool IsAttacking { get; private set; } = false;

    public bool IsAiming { get; private set; } = false;

    public event Action RollEvent;

    public event Action AimingEvent;
    public event Action SkillEvent;


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
        if (context.started)
        {
            IsAttacking = true;
            IsAiming = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
            IsAiming = false;
        }
    }

    // Rolling
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        RollEvent?.Invoke();
    }

    // Rotating
    public void OnAim(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        AimingEvent?.Invoke();
    }

    // Q Skill
    public void OnSkill(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        SkillEvent?.Invoke();
    }
    #endregion
}
