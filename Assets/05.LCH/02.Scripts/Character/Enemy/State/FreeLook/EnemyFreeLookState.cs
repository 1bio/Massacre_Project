using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreeLookState : EnemyBaseState
{
    public EnemyFreeLookState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
    }

    public override void Exit()
    {
    }
}
