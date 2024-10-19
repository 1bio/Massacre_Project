using UnityEngine;

public class PlayerRangeSkyFallState : PlayerRangeState
{
    public readonly int SkyFallAnimationHash = Animator.StringToHash("SkyFall@Range"); // 연발 애니메이션 해쉬


    public PlayerRangeSkyFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstract Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(SkyFallAnimationHash, CrossFadeDuration);

        stateMachine.InputReader.RollEvent += OnRolling;
        stateMachine.Health.ImpactEvent += OnImpact;
    }

    public override void Tick(float deltaTime)
    {
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
