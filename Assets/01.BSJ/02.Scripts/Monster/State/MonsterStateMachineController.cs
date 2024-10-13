using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStateMachineController : MonsterStateMachine
{
    public float CurrentBasicAttackCooldownTime { get; set; }
    public float CurrentSkillCooldownTime { get; set; }

    private void OnEnable()
    {
        OnSpawn();
    }

    private new void Update()
    {
        base.Update();

        CurrentBasicAttackCooldownTime += Time.deltaTime;
        CurrentSkillCooldownTime += Time.deltaTime;

        // 살아있는지 확인
        if (!IsAlive())
        {
            if (p_monster.MonsterStateType != MonsterStateType.Death)
                OnDead();
        }
        else
        {
            HandleLivingState();
        }
    }

    private void HandleLivingState()
    {
        // 다른 애니메이션이 실행되고 있는지 확인
        if (!p_monster.AnimationController.IsLockedInAnimation)
        {
            if (p_monster.MonsterStateType != MonsterStateType.Skill
                && p_monster.MonsterCombatController.MonsterCombatAbility.MonsterSkillData != null
                && Vector3.Distance(p_monster.MovementController.Astar.TargetTransform.position, this.transform.position) <= p_monster.MonsterCombatController.MonsterCombatAbility.MonsterSkillData.Range 
                && p_monster.MonsterCombatController.MonsterCombatAbility.MonsterSkillData.CooldownThreshold <= CurrentSkillCooldownTime)
            {
                OnSkill();
            }
            else if (p_monster.MonsterCombatController.IsTargetInRange)
            {
                if (p_monster.MonsterStateType != MonsterStateType.Idle
                    && p_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.CooldownThreshold > CurrentBasicAttackCooldownTime)
                    OnIdle();
                else
                {
                    OnAttack();
                }
            }
            else if (p_monster.MonsterStateType != MonsterStateType.Walk
                    && Vector3.Distance(p_monster.MovementController.Astar.TargetTransform.position, this.transform.position)
                    <= p_monster.MonsterCombatController.MonsterCombatAbility.MonsterTargetDistance.MaxTargetDistance)
            {
                OnMove();
            }
        }
    }

    private bool IsAlive()
    {
        if (p_monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth > 0)
        {
            return true;
        }
        else
        {
            p_monster.MonsterCombatController.MonsterCombatAbility.IsDead = true;
            return false;
        }
    }
}
