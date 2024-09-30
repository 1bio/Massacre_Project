using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediumImpactState : PlayerBaseState
{
    public readonly int ImpactBlendTree = Animator.StringToHash("ImpactBlendTree"); // 블렌드 트리

    public readonly int Impact = Animator.StringToHash("Impact"); // 애니메이션 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    public PlayerMediumImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactBlendTree, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(Impact, 2f, DampTime, deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        // 쿨타임동안 피격 당하지 않았을 경우
        if (currentInfo.normalizedTime >= 0.8f && stateMachine.Health.hitCount == 0)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }

        // 세 대 연속으로 피격 당할 경우
        if (stateMachine.Health.hitCount == 3)
        {
            stateMachine.ChangeState(new PlayerHardImpactState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }
    #endregion
}

