using System;
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
        _skillData = monster.MonsterSkillController.CurrentSkillData;
        _skillData.ActiveSkillEnter(monster);
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (!monster.MonsterStateMachineController.IsAlive())
            monster.MonsterStateMachineController.OnDead();

        monster.AnimationController.AnimatorStateInfo = monster.AnimationController.Animator.GetCurrentAnimatorStateInfo(0);
        _skillData.ActiveSkillTick(monster);
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        _skillData?.ActiveSkillExit(monster);
        monster.MonsterSkillController.CurrentSkillData.CooldownTimer = 0f;

        monster.MonsterParticleController?.AllStopVFXs();
        monster.MovementController.Astar?.StartPathCalculation();
    }
}
