﻿using UnityEngine;

public class PlayerRangeAimState : PlayerRangeState
{
    public readonly int AimAnimationHash = Animator.StringToHash("Aim@Range"); // 조준 애니메이션 해쉬


    public PlayerRangeAimState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstract Methods
    public override void Enter()
    {
        Aiming();

        stateMachine.Animator.CrossFadeInFixedTime(AimAnimationHash, CrossFadeDuration);
     
        stateMachine.InputReader.RollEvent += OnRolling;
        stateMachine.Health.ImpactEvent += OnImpact;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (currentInfo.normalizedTime > 0.8f)
        {
            stateMachine.ChangeState(new PlayerRangeState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.RollEvent -= OnRolling;
        stateMachine.Health.ImpactEvent -= OnImpact;
    }
    #endregion


    #region Event Methods
    private void OnRolling()
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
        return;
    }

    private void OnImpact()
    {
        stateMachine.ChangeState(new PlayerImpactState(stateMachine));
        return;
    }
    #endregion
}

