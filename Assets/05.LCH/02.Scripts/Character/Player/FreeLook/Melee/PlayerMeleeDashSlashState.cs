using UnityEngine;

public class PlayerMeleeDashSlashState : PlayerFreeLookState
{
    public readonly int DashSlashAnimationHash = Animator.StringToHash("DashSlash@Melee"); // ���� ���� �ִϸ��̼� �ؽ�

    private float Force;

    private float ForceTime;

    private float FrameTimeLimit = 1f;

    private float nextForceTime = 0.25f;

    private float nextForce = 0.03f;

    public PlayerMeleeDashSlashState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    #region abstarct Methods
    public override void Enter()
    {
        Aiming();

        SetForce();

        stateMachine.Animator.CrossFadeInFixedTime(DashSlashAnimationHash, CrossFadeDuration);

        stateMachine.Health.ImpactEvent += OnImpact;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        float normalizedTime = currentInfo.normalizedTime;

        // �ִϸ��̼ǿ� ���� �÷��̾� �ӵ� ����(���� ���� �ʿ�)
        if (normalizedTime >= 0f && FrameTimeLimit > normalizedTime)
        {
            if (ForceTime >= normalizedTime)
            {
                stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * Force);
            }
            else if(ForceTime + nextForceTime >= normalizedTime && normalizedTime > ForceTime)
            {
                stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * nextForce);
            }
        }

        if (currentInfo.normalizedTime > 0.8)
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


    #region Main Methods
    public void SetForce()
    {
        Force = DataManager.instance.playerData.skillData[3].force;
        ForceTime = DataManager.instance.playerData.skillData[3].forceTime;
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