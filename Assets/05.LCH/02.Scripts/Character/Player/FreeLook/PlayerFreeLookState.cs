﻿using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public readonly int FreeLookWithMelee = Animator.StringToHash("FreeLookWithMelee"); // 근거리 블렌드 트리

    public readonly int Velocity = Animator.StringToHash("Velocity"); // 애니메이션 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    public readonly float ExitTime = 0.8f;

    private int basicAttackDataIndex = 0; 

    private int heavyAttackDataIndex = 3;


    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    
    }

    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.InputReader.RollEvent += OnRolling;
        stateMachine.InputReader.AimingEvent += OnAiming;
        stateMachine.InputReader.SkillEvent += OnSkill;

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithMelee, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculatorMovement();

        Move(movement, deltaTime); // 이동

        Rotate(movement, deltaTime); // 회전

        if(Input.GetKeyDown(KeyCode.E)) { Swap(); }

        //Debug.Log(SkillManager.instance.GetRemainingCooldown("도약베기"));
        //Debug.Log(SkillManager.instance.GetRemainingCooldown("화염칼"));
        //Debug.Log(SkillManager.instance.GetRemainingCooldown("회전베기"));

        // Attacking
        if (stateMachine.InputReader.IsAttacking)
        {
            if (SkillManager.instance.IsPassiveActive("화염칼")) // 화염칼 [4]
            {
                stateMachine.ChangeState(new PlayerHeavyAttackState(stateMachine, heavyAttackDataIndex));
                return;
            }
            else
            {
                stateMachine.ChangeState(new PlayerMeleeAttackState(stateMachine, basicAttackDataIndex));
                return;
            }
        }

        // Idle & Moving
        if (stateMachine.InputReader.MoveValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(Velocity, 0f, DampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(Velocity, 1f, DampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.RollEvent -= OnRolling;
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

            stateMachine.ChangeState(new PlayerRangeFreeLookState(stateMachine));
        }
    }
    #endregion


    #region Event Methods
    private void OnRolling()
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
    }

    private void OnAiming() // 도약베기 [3]
    {
        if (SkillManager.instance.GetRemainingCooldown("도약베기") <= 0f && !DataManager.instance.playerData.skillData[3].isUnlock)
        {
            stateMachine.ChangeState(new PlayerMeleeDashSlashState(stateMachine));
        }
    }

    private void OnSkill() // 회전베기 [5]
    {
        if(SkillManager.instance.GetRemainingCooldown("회전베기") <= 0f && !DataManager.instance.playerData.skillData[5].isUnlock)
        {
            stateMachine.ChangeState(new PlayerMeleeSpinSlashState(stateMachine));
        }
    }
    #endregion
}
