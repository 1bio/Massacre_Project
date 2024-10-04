using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterBehaviourAttack : MonsterBehaviour
{
    private bool _isTurning = false;


    public override void OnBehaviourStart(Monster monster)
    {
        monster.IsLockedInAnimation = true;
        LookAtTarget(monster);
        monster.SetRandomAttackAnimation(3);
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        monster.UnLockAnimation(monster.CurrentAniamtionName);
    }   

    public override void OnBehaviourEnd(Monster monster)
    {
        monster.Astar.StartPathCalculation();
    }

    private void LookAtTarget(Monster monster)
    {
        _isTurning = true;
        Vector3 targetPos = monster.Astar.TargetTransform.position;
        Vector3 direction = (targetPos - monster.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.MonsterAbility.TurnSpeed * Time.deltaTime);
        _isTurning = false;
    }
}
