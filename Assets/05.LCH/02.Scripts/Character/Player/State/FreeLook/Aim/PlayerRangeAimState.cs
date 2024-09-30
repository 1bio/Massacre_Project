using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAimState : PlayerBaseState
{
    public PlayerRangeAimState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Aim!");
    }

    public override void Tick(float deltaTime)
    {
        // 공격 상태로 전이 가능
        // 우클릭 해제 시 FreeLookState로
    }

    public override void Exit()
    {
    }
}
