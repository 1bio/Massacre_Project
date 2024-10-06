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

        stateMachine.Health.ImpactEvent += OnImpact;
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if(currentInfo.normalizedTime > 0.8f)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Health.ImpactEvent -= OnImpact;
    }
    #endregion


    #region Event Methods
    private void OnImpact()
    {
        stateMachine.ChangeState(new PlayerImpactState(stateMachine));
        return;
    }
    #endregion
}
