﻿using System;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public readonly int FreeLookWithMelee = Animator.StringToHash("FreeLookWithMelee"); // 근거리 블렌드 트리

    public readonly int Velocity = Animator.StringToHash("Velocity"); // 애니메이션 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    private int basicAttackDataIndex = 0; 

    private int heavyAttackDataIndex = 3;

    private float heavyAttackDurationTime = 10f;

    private float heavyAttackEnterTime;

    private float currentTime;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithMelee, CrossFadeDuration);

        stateMachine.InputReader.RollEvent += OnRolling; // 구르기 
        stateMachine.Health.ImpactEvent += OnImpact; // 피격 

        stateMachine.InputReader.AimingEvent += OnAiming; // 도약베기
        stateMachine.InputReader.SkillEvent += OnSkill; // 회전베기
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculatorMovement();

        Move(movement, deltaTime); // 이동

        Rotate(movement, deltaTime); // 회전

        if (Input.GetKeyDown(KeyCode.E)) { Swap(); }

        /*Debug.Log($"회전베기 남은 쿨타임: {stateMachine.CoolDownController.GetRemainingCooldown("회전베기")}");*/

        currentTime += Time.time;

        // Attack
        if (stateMachine.InputReader.IsAttacking && stateMachine.WeaponPrefabs[0].activeSelf)
        {
            if (stateMachine.CoolDownController.GetRemainingCooldown("미정") <= 0 && currentTime - heavyAttackDurationTime >= heavyAttackEnterTime)
            {
                heavyAttackEnterTime = Time.time;

                stateMachine.ChangeState(new PlayerHeavyAttackState(stateMachine, heavyAttackDataIndex));
                return;
            }
            if (stateMachine.CoolDownController.GetRemainingCooldown("미정") > 0)
            {
                stateMachine.ChangeState(new PlayerMeleeAttackState(stateMachine, basicAttackDataIndex));
                return;
            }
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

        stateMachine.InputReader.AimingEvent -= OnAiming;
        stateMachine.InputReader.SkillEvent -= OnSkill;
    }
    #endregion


    #region Main Methods
    // 무기 스왑
    private void Swap()
    {
        if (stateMachine.WeaponPrefabs[0].activeSelf)
        {
            // Range Weapon
            stateMachine.WeaponPrefabs[0].SetActive(false); // 근접 무기 비활성화
            stateMachine.WeaponPrefabs[1].SetActive(true);  // 원거리 무기 활성화

            stateMachine.ChangeState(new PlayerRangeState(stateMachine));
            return;
        }
    }
    #endregion


    #region Event Methods
    private void OnRolling() // 구르기
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
        return;
    }

    private void OnImpact() // 피격
    {
        if (stateMachine.Health.hitCount == 1)
        {
            stateMachine.ChangeState(new PlayerImpactState(stateMachine));
            return;
        }
    }

    private void OnAiming() // 도약베기
    {
        if (stateMachine.CoolDownController.GetRemainingCooldown("도약베기") <= 0f)
        {
            stateMachine.ChangeState(new PlayerMeleeDashSlashState(stateMachine));
            return;
        }
    }

    private void OnSkill() // 회전베기
    {
        if(stateMachine.CoolDownController.GetRemainingCooldown("회전베기") <= 0f)
        {
            stateMachine.ChangeState(new PlayerMeleeSpinSlashState(stateMachine));
            return;
        }
    }
    #endregion
}
