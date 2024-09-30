using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackState : PlayerBaseState
{
    public readonly int AttackAnimationHash = Animator.StringToHash("Attack@Bow"); // 근거리 블렌드 트리

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    public PlayerRangeAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstract Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackAnimationHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (!stateMachine.InputReader.IsAttacking && currentInfo.normalizedTime >= 0.8f)
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
