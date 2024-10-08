using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStateMachineController : MonsterStateMachine
{
    private void OnEnable()
    {
        OnSpawn();
    }

    private new void Update()
    {
        base.Update();

        p_monster.MonsterCombatController.BasicAttackCoolTimeCheck += Time.deltaTime;
        p_monster.MonsterCombatController.SkillAttackCoolTimeCheck += Time.deltaTime;

        // ����ִ��� Ȯ��
        if (!IsAlive())
        {
            if (p_monster.MonsterStateType != MonsterStateType.Dead)
                OnDead();
        }
        else
        {
            HandleLivingState();
        }
    }

    private void HandleLivingState()
    {
        // �ٸ� �ִϸ��̼��� ����ǰ� �ִ��� Ȯ��
        if (!p_monster.AnimationController.IsLockedInAnimation)
        {
            if (p_monster.MonsterCombatController.IsTargetInRange)
            {
                if (p_monster.MonsterCombatController.BasicAttackCoolTimeCheck <= p_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.AttackCooldown)
                    OnIdle();
                else
                {
                    OnAttack();
                    p_monster.MonsterCombatController.BasicAttackCoolTimeCheck = 0;
                }
            }
            else if (p_monster.MonsterStateType != MonsterStateType.Movement
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
