using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourSkill : MonsterBehaviour
{
    private Monster _monster;
    private MonsterSkillData _skillData;

    public override void OnBehaviourStart(Monster monster)
    {
        monster.AnimationController.IsLockedInAnimation = true;

        _monster = monster;
        monster.MonsterCombatController.Health.ImpactEvent += OnImpact;
        _skillData = monster.MonsterCombatController.MonsterCombatAbility.MonsterSkillData.CreateInstance();

        _skillData.ActiveSkillEnter(monster);
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        monster.AnimationController.AnimatorStateInfo = monster.AnimationController.Animator.GetCurrentAnimatorStateInfo(0);
        _skillData.ActiveSkillTick(monster);
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        _skillData.ActiveSkillExit(monster);
        monster.MonsterStateMachineController.CurrentSkillCooldownTime = 0;

        monster.MovementController.Astar.StartPathCalculation();

        monster.MonsterCombatController.Health.ImpactEvent -= OnImpact;
    }

    private void OnImpact()
    {

    }
}
