using UnityEngine;

public class PlayerRangeAttackState : PlayerRangeFreeLookState
{
    private AttackData attack;

    public readonly int AttackAnimationHash = Animator.StringToHash("Attack@Range"); // 원거리 공격 애니메이션 해쉬

    public PlayerRangeAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstract Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AttackAnimationHash, CrossFadeDuration);

        stateMachine.InputReader.RollEvent += OnRolling;
    }

    public override void Tick(float deltaTime)
    {
        Aiming();

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (!stateMachine.InputReader.IsAttacking && currentInfo.normalizedTime > 1.0f)
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
