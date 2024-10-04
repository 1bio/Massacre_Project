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
        
        // ����ִ��� Ȯ��
        if (!IsAlive())
        {
            OnDead();
        }
        else
        {
            // �ٸ� �ִϸ��̼��� ����ǰ� �ִ��� Ȯ��
            if (!_monster.IsLockedInAnimation)
            {
                if (Vector3.Distance(_monster.Astar.TargetTransform.position, this.transform.position) <= _monster.MonsterAbility.MonsterTargetDistance.MaxTargetDistance)
                {
                    OnAttack();
                }
                else
                {
                    if (_monster.MonsterAbility.MonsterHealth.CurrentHealth != _monster.MonsterAbility.MonsterHealth.LastHealth)
                    {
                        OnGotHit();
                        _monster.MonsterAbility.MonsterHealth.LastHealth = _monster.MonsterAbility.MonsterHealth.CurrentHealth;
                    }

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

        if (_monster.MonsterAbility.MonsterHealth.CurrentHealth > 0)
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
