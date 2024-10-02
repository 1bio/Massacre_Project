using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class MonsterStateMachineController : MonsterStateMachine
{
    private void Start()
    {
        OnIdle();
    }

    private new void Update()
    {
        base.Update();
        
        // 살아있는지 확인
        if (!IsAlive())
        {
            OnDead();
        }
        else
        {
            // 다른 애니메이션이 실행되고 있는지 확인
            if (!_monster.IsLockedInAnimation)
            {
                if (Vector3.Distance(_monster.Astar.TargetTransform.position, this.transform.position) <= _monster.MonsterAbility.MonsterTargetDistance.MaxTargetDistance)
                {
                    OnAttack();
                }
                else
                {
                    if (_monster.MonsterStateType != MonsterStateType.Movement)
                        OnMove();
                }
            }
        }
    }

    private bool IsAlive()
    {
        if (_monster.MonsterAbility == null)
        {
            _monster.MonsterAbility = new MonsterCombatAbility(_monster.MonsterStatData);
        }


        if (_monster.MonsterAbility.MonsterHealth.Health > 0)
        {
            return true;
        }
        else
        {
            _monster.MonsterAbility.IsDead = true;
            return false;
        }
    }
}
