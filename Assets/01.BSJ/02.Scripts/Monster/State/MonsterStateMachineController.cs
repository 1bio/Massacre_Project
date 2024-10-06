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


        p_monster.BasicAttackCoolTimeCheck += Time.deltaTime;
        p_monster.SkillAttackCoolTimeCheck += Time.deltaTime;

        // 살아있는지 확인
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
        // 다른 애니메이션이 실행되고 있는지 확인
        if (!p_monster.IsLockedInAnimation)
        {
            if (Vector3.Distance(p_monster.Astar.TargetTransform.position, this.transform.position) <= p_monster.MonsterAbility.MonsterTargetDistance.IdealTargetDistance)
            {
                if (p_monster.BasicAttackCoolTimeCheck <= p_monster.MonsterAbility.MonsterAttack.AttackCooldown)
                    OnIdle();
                else
                {
                    OnAttack();
                    p_monster.BasicAttackCoolTimeCheck = 0;
                }
            }
            else
            {
                if (p_monster.MonsterAbility.MonsterHealth.IsHit)
                    OnGotHit();
                else if (p_monster.MonsterStateType != MonsterStateType.Movement
                    && Vector3.Distance(p_monster.Astar.TargetTransform.position, this.transform.position) <= p_monster.MonsterAbility.MonsterTargetDistance.MaxTargetDistance)
                    OnMove();
            }
        }
    }

    private bool IsAlive()
    {
        if (p_monster.MonsterAbility == null)
        {
            p_monster.MonsterAbility = new MonsterCombatAbility(p_monster.MonsterStatData);
        }

        if (p_monster.MonsterAbility.MonsterHealth.CurrentHealth > 0)
        {
            return true;
        }
        else
        {
            p_monster.MonsterAbility.IsDead = true;
            return false;
        }
    }
}
