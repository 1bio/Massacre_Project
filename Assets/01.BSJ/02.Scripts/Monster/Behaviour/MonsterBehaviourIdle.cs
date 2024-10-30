using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourIdle : MonsterBehaviour
{
    private Monster _monster;
    public override void OnBehaviourStart(Monster monster)
    {
        _monster = monster;
        _monster.MonsterCombatController.Health.SetHealth(monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth);

        monster.AnimationController.PlayIdleAnimation();

        monster.MonsterCombatController.Health.ImpactEvent += OnImpact;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);
        monster.MovementController.Astar.StartPathCalculation(monster.transform.position, monster.MovementController.Astar.TargetTransform.position);
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        monster.MonsterCombatController.Health.ImpactEvent -= OnImpact;
    }

    private void OnImpact()
    {
        _monster.MonsterStateMachineController.OnGotHit();
    }
}
