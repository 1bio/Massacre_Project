using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{
    public readonly int RollAnimationHash = Animator.StringToHash("Roll");

    public readonly float CrossFadeDuration = 0.1f;

    private float currentTime;

    private float lastTime;


    public PlayerRollingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    #region abstract Methods 
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(RollAnimationHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.ForceReceiver.RollingForce(deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        // FreeLook
        if (currentInfo.normalizedTime >= 0.8f)
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
