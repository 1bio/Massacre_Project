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
        // ���� ���·� ���� ����
        // ��Ŭ�� ���� �� FreeLookState��
    }

    public override void Exit()
    {
    }
}
