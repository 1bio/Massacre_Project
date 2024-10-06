using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehaviourAttack : MonsterBehaviour
{
    private bool _hasAttacked;

    public override void OnBehaviourStart(Monster monster)
    {
        _hasAttacked = false;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (monster.IsLockedInAnimation)
        {
            /*monster.UnLockAnimation(monster.CurrentAniamtionName);*/
        }
        else
        {
            monster.LookAtTarget(monster);
        }

        if (Vector3.Angle(monster.transform.forward, monster.Direction) <= 20f && !_hasAttacked)
        {
            monster.SetRandomAttackAnimation();
            _hasAttacked = true;
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        monster.Astar.StartPathCalculation();
    }
}
