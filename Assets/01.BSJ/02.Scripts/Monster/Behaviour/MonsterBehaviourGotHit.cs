using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourGotHit : MonsterBehaviour
{
    private Monster _monster;

    private float moveSpeed;

    public override void OnBehaviourStart(Monster monster)
    {
        _monster = monster;
        _monster.CombatController.Health.SetHealth(monster.CombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth);

        moveSpeed = _monster.CombatController.MonsterCombatAbility.MoveSpeed;

        monster.AnimationController.PlayGotHitAnimation();

        monster.MovementController.TargetDetector.IsTargetDetected = true;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        /*Move(Time.deltaTime);*/
    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }


    // 이동
    private void Move(Vector3 motion, float deltaTime)
    {
        _monster.Controller.Move((motion + _monster.ForceReceiver.movement) * moveSpeed * deltaTime);
    }

    // 이동(넉백과 같은 물리적인 힘의 이동)
    private void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

}
