using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttackState : PlayerBaseState
{
    private AttackData attack;

    public readonly int AttackAnimationHash = Animator.StringToHash("Attack@Range"); // 원거리 공격 애니메이션 해쉬

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    public PlayerRangeAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstract Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackAnimationHash, CrossFadeDuration);

        stateMachine.Health.ImpactEvent += OnImpact;
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (!stateMachine.InputReader.IsAttacking && currentInfo.normalizedTime > 0.8f)
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
