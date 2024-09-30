using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardImpactState : PlayerBaseState
{
    public readonly int ImpactBlendTree = Animator.StringToHash("ImpactBlendTree"); // 블렌드 트리

    public readonly int Impact = Animator.StringToHash("Impact"); // 애니메이션 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    public PlayerHardImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactBlendTree, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(Impact, 3f, DampTime, deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        
        // 애니메이션 재생이 끝난 후
        if (currentInfo.normalizedTime >= 1f)
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

