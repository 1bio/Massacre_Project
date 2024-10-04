using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAimState : PlayerBaseState
{
    public readonly int AimAnimationHash = Animator.StringToHash("Aim@Range"); // ���� �ִϸ��̼� �ؽ�

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;


    public PlayerRangeAimState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstract Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AimAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        Aiming();
        
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if(!stateMachine.InputReader.IsAiming || currentInfo.normalizedTime >= 1.0f)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {

    }
    #endregion

}

