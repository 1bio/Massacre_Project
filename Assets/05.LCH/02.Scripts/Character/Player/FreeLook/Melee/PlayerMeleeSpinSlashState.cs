using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeSpinSlashState : PlayerFreeLookState
{
    public readonly int SpinSlashAnimationHash = Animator.StringToHash("SpinSlash@Melee"); // 스핀 공격 애니메이션 해쉬

    private float Force;

    private float ForceTime;

    private float FrameTimeLimit = 1f;


    public PlayerMeleeSpinSlashState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstarct Methods
    public override void Enter()
    {
        SetForce();

        stateMachine.Animator.CrossFadeInFixedTime(SpinSlashAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        float normalizedTime = currentInfo.normalizedTime;

        if (normalizedTime >= 0f && FrameTimeLimit > normalizedTime)
        {
            if (ForceTime >= normalizedTime)
            {
                stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * Force);
            }
        }

        if (!stateMachine.InputReader.IsAttacking && normalizedTime > 0.8)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }
    #endregion


    #region Main Methods
    public void SetForce()
    {
        Force = DataManager.instance.playerData.skillData[5].force;
        ForceTime = DataManager.instance.playerData.skillData[5].forceTime;
    }
    #endregion
}
