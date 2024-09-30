﻿using System;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public readonly int FreeLookWithMelee = Animator.StringToHash("FreeLookWithMelee"); // 근거리 블렌드 트리

    public readonly int FreeLookWithRange = Animator.StringToHash("FreeLookWithRange"); // 원거리 블렌드 트리

    private readonly int Change = Animator.StringToHash("Change");

    public readonly int Velocity = Animator.StringToHash("Velocity"); // 애니메이션 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    private bool IsChanged;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstarct Methods
    public override void Enter()
    {
        if (IsChanged)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithMelee, CrossFadeDuration);

            stateMachine.Targeting.Collider.radius = stateMachine.MeleeWeaponDetectionRange;
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithRange, CrossFadeDuration);

            stateMachine.Targeting.Collider.radius = stateMachine.RangeWeaponDetectionRange;
        }

        stateMachine.InputReader.RollEvent += OnRolling; // Rolling 
        stateMachine.Health.ImpactEvent += OnImpact; // Impact 
    }

   
    public override void Tick(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Swap();
        }

        IsChanged = stateMachine.WeaponPrefabs[1].activeSelf; // 원거리 무기가 활성화면 IsChanged = true

        Vector3 movement = CalculatorMovement();

        Move(movement, deltaTime); // 이동

        Rotate(movement, deltaTime); // 회전

        /*  Aiming(); // 조준
          AutoRotate(deltaTime); // 자동 회전*/

        // Attacking
        if (stateMachine.InputReader.IsAttacking)
        {
            if (!IsChanged && stateMachine.WeaponPrefabs[0].activeSelf) // Melee
            {
                stateMachine.ChangeState(new PlayerMeleeAttackState(stateMachine, 0));
                return;
            }
            else if(IsChanged && stateMachine.WeaponPrefabs[1].activeSelf) // Range
            {
                stateMachine.ChangeState(new PlayerRangeAttackState(stateMachine));
                return;
            }
        }

        // Aiming(Range)
        if (stateMachine.InputReader.IsAiming && IsChanged)
        {
            stateMachine.ChangeState(new PlayerRangeAimState(stateMachine));
            return;
        }

        // Idling
        if (stateMachine.InputReader.MoveValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(Velocity, 0f, DampTime, deltaTime);
            return;
        }

        // Moving
        stateMachine.Animator.SetFloat(Velocity, 1f, DampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.RollEvent -= OnRolling;
        stateMachine.Health.ImpactEvent -= OnImpact;
    }
    #endregion


    #region Main Methods
    private void Swap()
    {
        if (stateMachine.WeaponPrefabs[0].activeSelf) 
        {
            // Range Weapon
            stateMachine.WeaponPrefabs[0].SetActive(false); // 근접 무기 비활성화
            stateMachine.WeaponPrefabs[1].SetActive(true);  // 원거리 무기 활성화

            stateMachine.Animator.SetBool(Change, true);    
            IsChanged = true;                          
        }
        else 
        {
            // Melee Weapon
            stateMachine.WeaponPrefabs[0].SetActive(true);  // 근접 무기 활성화
            stateMachine.WeaponPrefabs[1].SetActive(false); // 원거리 무기 비활성화

            stateMachine.Animator.SetBool(Change, false);  
            IsChanged = false;                         
        }
    }
    #endregion



    #region Event Methods
    private void OnRolling()
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
        return;
    }

    private void OnImpact()
    {
        if(stateMachine.Health.hitCount == 1)
        {
            stateMachine.ChangeState(new PlayerLightImpactState(stateMachine));
            return;
        }
    }
    #endregion
}
