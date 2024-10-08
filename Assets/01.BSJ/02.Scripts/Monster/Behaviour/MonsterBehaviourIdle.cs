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
        monster.AnimationController.PlayIdleAnimation();
        
        monster.MonsterCombatController.Health.ImpactEvent += OnImpact;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        monster.MovementController.LookAtTarget(monster.MovementController.Astar.TargetTransform);
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
