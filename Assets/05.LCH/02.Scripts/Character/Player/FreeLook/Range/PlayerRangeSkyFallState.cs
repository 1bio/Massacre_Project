using UnityEngine;

public class PlayerRangeSkyFallState : PlayerRangeFreeLookState
{
    public readonly int SkyFallAnimationHash = Animator.StringToHash("SkyFall@Range"); // 연발 애니메이션 해쉬


    public PlayerRangeSkyFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstract Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(SkyFallAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
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
