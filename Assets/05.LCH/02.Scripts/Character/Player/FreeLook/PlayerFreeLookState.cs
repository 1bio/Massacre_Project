using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    // BlendTree 애니메이션 변수
    public readonly int FreeLookWithMelee = Animator.StringToHash("FreeLookWithMelee"); // 근거리 블렌드 트리

    public readonly int FreeLookWithRange = Animator.StringToHash("FreeLookWithRange"); // 원거리 블렌드 트리

    public readonly int Velocity = Animator.StringToHash("Velocity"); // 애니메이션 파라미터

    private readonly int Change = Animator.StringToHash("Change"); // 무기 스왑 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    private bool IsChanged; // false = Melee Weapon, true = Range Weapon


    private CoolDownController coolDownManager; 

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        coolDownManager = stateMachine.GetComponent<CoolDownController>();
    }

    #region abstarct Methods
    public override void Enter()
    {
        if (IsChanged)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithMelee, CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithRange, CrossFadeDuration);
        }

        stateMachine.InputReader.RollEvent += OnRolling; // Rolling 
        stateMachine.InputReader.SkillEvent += OnSkill; // Impact 
        stateMachine.Health.ImpactEvent += OnImpact; // Impact 
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculatorMovement();

        Move(movement, deltaTime); // 이동

        Rotate(movement, deltaTime); // 회전

        WeaponSetting();

        // Attacking
        if (stateMachine.InputReader.IsAttacking)
        {
            if (!IsChanged && stateMachine.WeaponPrefabs[0].activeSelf) // Melee
            {
                stateMachine.ChangeState(new PlayerMeleeAttackState(stateMachine, 0));
                return;
            }
            else if (IsChanged && stateMachine.WeaponPrefabs[1].activeSelf) // Range
            {
                // RapidShot
                if (!coolDownManager.IsSkillOnCooldown("RapidShot")) // 쿨타임이 아니면
                {
                    coolDownManager.StartCooldown("RapidShot", DataManager.instance.playerData.skillData[1].coolDown);
                 
                    stateMachine.ChangeState(new PlayerRangeRapidShotState(stateMachine));
                    return;
                }

                // 쿨타임일 경우 기본 공격 
                stateMachine.ChangeState(new PlayerRangeAttackState(stateMachine));
                return;
            }
        }


        // <==== Skill State====>
        // 마우스 우클릭 스킬
        if (stateMachine.InputReader.IsAiming && IsChanged) // 원거리 스킬
        {
            if (!coolDownManager.IsSkillOnCooldown("Aiming")) 
            {
                coolDownManager.StartCooldown("Aiming", DataManager.instance.playerData.skillData[0].coolDown); // 쿨타임 시작
                stateMachine.ChangeState(new PlayerRangeAimState(stateMachine));
                return;
            }
            else if (coolDownManager.IsSkillOnCooldown("Aiming")) // 쿨타임일 경우
            {
                Debug.Log("쿨타임 입니다!");
            }
        }
        else if(stateMachine.InputReader.IsAiming && !IsChanged) // 근거리 스킬
        {
            stateMachine.ChangeState(new PlayerMeleeDashSlashState(stateMachine));
            return;
        }

        

        // <=====  Locomotion State =====>
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
        stateMachine.InputReader.SkillEvent -= OnSkill;
        stateMachine.Health.ImpactEvent -= OnImpact;
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

    private void WeaponSetting()
    {
        if (Input.GetKeyDown(KeyCode.E)) { Swap(); }

        IsChanged = stateMachine.WeaponPrefabs[1].activeSelf; // 원거리 무기 활성화 => IsChanged = true
    }
    #endregion


    #region Event Methods
    private void OnRolling() // 구르기
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
        return;
    } 

    private void OnSkill() // Q 스킬(근거리 및 원거리)
    {
        if (IsChanged)
        {
            stateMachine.ChangeState(new PlayerRangeSkyFallState(stateMachine));
            return;
        }
        else
        {
            stateMachine.ChangeState(new PlayerMeleeSpinSlashState(stateMachine));
            return;
        }
    }   

    private void OnImpact() // 피격
    {
        if(stateMachine.Health.hitCount == 1)
        {
            stateMachine.ChangeState(new PlayerImpactState(stateMachine));
            return;
        }
    } 
    #endregion
}
