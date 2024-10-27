using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStateMachineController : MonsterStateMachine
{
    public float CurrentBasicAttackCooldownTime { get; set; }

    private void OnEnable()
    {
        OnSpawn();
    }

    private new void Update()
    {
        base.Update();

        CurrentBasicAttackCooldownTime += Time.deltaTime;
        p_monster.MonsterSkillController.UpdateCooldowns();

        // 살아있는지 확인
        if (!IsAlive())
        {
            if (p_monster.MonsterStateType != MonsterStateType.Death)
                OnDead();

            // 사라지지 않았을 때
            StartCoroutine(DeathCheck());
        }
        else if (p_monster.MovementController.TargetDetector.IsTargetDetected)
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
                && p_monster.MonsterSkillController.GetAvailableSkills().Count > 0
                && p_monster.MonsterSkillController.UpdateCurrentSkillData().Range >= Vector3.Distance(p_monster.MovementController.Astar.TargetTransform.position, this.transform.position))
            {

                OnSkill();
            }
            else if (p_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.IsTargetWithinAttackRange ||
                    Vector3.Distance(p_monster.MovementController.Astar.TargetTransform.position, this.transform.position)
                    <= p_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.Range)
            {
                if (p_monster.MonsterStateType != MonsterStateType.Idle
                    && p_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.CooldownThreshold > CurrentBasicAttackCooldownTime)
                    OnIdle();
                else
                {
                    OnAttack();
                }
            }
            else if (p_monster.MonsterStateType != MonsterStateType.Walk)
            {
                OnMove();
            }
        }
    }

    public bool IsAlive()
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

    private IEnumerator DeathCheck()
    {
        yield return new WaitForSeconds(3f);

        if(p_monster.gameObject.activeSelf)
            p_monster.gameObject.SetActive(false);
    }
}
