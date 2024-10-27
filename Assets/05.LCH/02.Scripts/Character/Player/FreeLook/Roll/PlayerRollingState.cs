using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{
    public readonly int RollAnimationHash = Animator.StringToHash("Roll");

    public readonly float CrossFadeDuration = 0.1f;

    private Vector2 DodgeDirectionInput;

    private float remainingDodgeTime;

    private float currentTime;

    private float lastTime;


    public PlayerRollingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    #region abstract Methods 
    public override void Enter()
    {
        Debug.Log("123");

        stateMachine.Health.SetInvulnerable(true);

        stateMachine.Animator.CrossFadeInFixedTime(RollAnimationHash, CrossFadeDuration);

        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown)
            return;

        stateMachine.SetDodgeTime(Time.time);

        DodgeDirectionInput = stateMachine.InputReader.MoveValue;

        remainingDodgeTime = stateMachine.DodgeDuration;
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.ForceReceiver.RollingForce(deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        // FreeLook
        if (currentInfo.normalizedTime > 0.8f && stateMachine.WeaponPrefabs[0].activeSelf)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }
        else if(currentInfo.normalizedTime > 0.8f && stateMachine.WeaponPrefabs[1].activeSelf)
        {
            stateMachine.ChangeState(new PlayerRangeFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }
    #endregion
}
