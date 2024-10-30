using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehaviourAttack : MonsterBehaviour
{
    private Monster _monster;
    private bool _hasAttacked = false;
    private float _currentTime = 0;
    private float _attackAngleThreshold = 5f;

    public override void OnBehaviourStart(Monster monster)
    {
        _monster = monster;
        _monster.MonsterCombatController.Health.SetHealth(monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth);

        monster.MonsterCombatController.Health.ImpactEvent += OnImpact;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (!monster.MonsterStateMachineController.IsAlive())
            monster.MonsterStateMachineController.OnDead();

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
        monster.MovementController.Astar.StartPathCalculation(monster.transform.position, monster.MovementController.Astar.TargetTransform.position);
        monster.MonsterStateMachineController.CurrentBasicAttackCooldownTime = 0;

        monster.MonsterCombatController.Health.ImpactEvent -= OnImpact;
    }

    private void OnImpact()
    {
        if (!_monster.AnimationController.IsLockedInAnimation)
        {
            _monster.MonsterStateMachineController.OnGotHit();
        }
        //_monster.MonsterStateMachineController.OnGotHit();
    }
}
