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
        _monster.MonsterCombatController.Health.SetHealth(monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth);

        moveSpeed = _monster.MonsterCombatController.MonsterCombatAbility.MoveSpeed;

        monster.AnimationController.PlayGotHitAnimation();

        monster.MovementController.TargetDetector.IsTargetDetected = true;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (!monster.MonsterStateMachineController.IsAlive())
            monster.MonsterStateMachineController.OnDead();
        /*Move(Time.deltaTime);*/
    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }


    // �̵�
    private void Move(Vector3 motion, float deltaTime)
    {
        _monster.Controller.Move((motion + _monster.ForceReceiver.movement) * moveSpeed * deltaTime);
    }

    // �̵�(�˹�� ���� �������� ���� �̵�)
    private void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

}
