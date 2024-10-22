using UnityEngine;

public class PlayerRangeAimState : PlayerRangeFreeLookState
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
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (currentInfo.normalizedTime > 0.8f)
        {
            stateMachine.ChangeState(new PlayerRangeFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }
    #endregion
}

