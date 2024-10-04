using System;
using System.Collections;
using UnityEngine;

public class PlayerRangeRapidShotState : PlayerBaseState
{
    public readonly int RapidShotAnimationHash = Animator.StringToHash("RapidShot@Range"); // 연발 애니메이션 해쉬

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;


    public PlayerRangeRapidShotState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(RapidShotAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if(currentInfo.normalizedTime >= 0.8f)
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
