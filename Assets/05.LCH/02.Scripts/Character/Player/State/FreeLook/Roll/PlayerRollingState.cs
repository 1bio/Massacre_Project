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

        lastTime = stateMachine.RollingCoolTime;
    }


    public override void Tick(float deltaTime)
    {
        /*CheckCoolTime(); // ��Ÿ�� üũ*/

        stateMachine.ForceReceiver.RollingForce(deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        // FreeLook
        if (currentInfo.normalizedTime >= 0.8f)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }

        // �ǰ� ���� �ÿ� �´� ���·� ����

    }

    public override void Exit()
    {
    }
    #endregion


    #region Main Methods
    public void CheckCoolTime()
    {
        currentTime = Time.time;

        if (currentTime - lastTime >= stateMachine.RollingCoolTime)
        {
            stateMachine.ChangeState(new PlayerRollingState(stateMachine));
            return;
        }

        lastTime = currentTime;

        stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
        return;
    }
    #endregion

}
