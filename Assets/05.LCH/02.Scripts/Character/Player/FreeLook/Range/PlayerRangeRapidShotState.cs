using UnityEngine;

public class PlayerRangeRapidShotState : PlayerRangeFreeLookState
{
    public readonly int RapidShotAnimationHash = Animator.StringToHash("RapidShot@Range"); // 연발 애니메이션 해쉬


    public PlayerRangeRapidShotState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(RapidShotAnimationHash, CrossFadeDuration);

        stateMachine.InputReader.RollEvent += OnRolling;
    }

    public override void Tick(float deltaTime)
    {
        Aiming();

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if(currentInfo.normalizedTime > 0.8f)
        {
            stateMachine.ChangeState(new PlayerRangeFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.RollEvent -= OnRolling;
    }
    #endregion


    #region Event Methods
    private void OnRolling()
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
        return;
    }
    #endregion
}
