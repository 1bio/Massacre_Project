using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehaviourAttack : MonsterBehaviour
{
    private Monster _monster;
    private bool _hasAttacked = false;
    private float _currentTime = 0;
    private float _attackAngleThreshold = 20f;

    public override void OnBehaviourStart(Monster monster)
    {
        _monster = monster;

        //monster.MonsterCombatController.Health.ImpactEvent += OnImpact;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        _currentTime += Time.deltaTime;

        if (!monster.AnimationController.IsLockedInAnimation)
        {
            monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);
        }

        if (Vector3.Angle(monster.transform.forward, monster.MovementController.Direction) <= _attackAngleThreshold && !_hasAttacked)
        {
            monster.AnimationController.PlayAttackAnimation(monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.TotalCount);
            
            _hasAttacked = true;
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        monster.MovementController.Astar.StartPathCalculation();
        monster.MonsterStateMachineController.CurrentBasicAttackCooldownTime = 0;

        //monster.MonsterCombatController.Health.ImpactEvent -= OnImpact;
    }

    private void OnImpact()
    {
        if (Vector3.Angle(_monster.transform.forward, _monster.MovementController.Direction) > _attackAngleThreshold)
        {
            _monster.MonsterStateMachineController.OnGotHit();
        }
    }
}
