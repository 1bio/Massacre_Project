using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehaviourAttack : MonsterBehaviour
{
    private Monster _monster;
    private bool _hasAttacked;

    public override void OnBehaviourStart(Monster monster)
    {
        _monster = monster;
        _hasAttacked = false;

        monster.MonsterCombatController.Health.ImpactEvent += OnImpact;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (!monster.AnimationController.IsLockedInAnimation)
        {
            monster.MovementController.LookAtTarget(monster.MovementController.Astar.TargetTransform);
        }

        if (Vector3.Angle(monster.transform.forward, monster.MovementController.Direction) <= 30f && !_hasAttacked)
        {
            monster.AnimationController.PlayAttackAnimation(3);
            _hasAttacked = true;
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        monster.MovementController.Astar.StartPathCalculation();

        monster.MonsterCombatController.Health.ImpactEvent -= OnImpact;
    }

    private void OnImpact()
    {
        // 체력 감소
    }
}
