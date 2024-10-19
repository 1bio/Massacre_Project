using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    public readonly int GetHit = Animator.StringToHash("GetHit"); // 블렌드 트리

    public readonly int Impact = Animator.StringToHash("Impact"); // 애니메이션 해쉬

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    public readonly int doubleHits = 2;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(GetHit, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(Impact, 1f, DampTime, deltaTime);

        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        // FreeLook
        if (currentInfo.normalizedTime >= 0.8f && stateMachine.WeaponPrefabs[0].activeSelf && stateMachine.Health.hitCount == 0)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }
        else if (currentInfo.normalizedTime >= 0.8f && stateMachine.WeaponPrefabs[1].activeSelf && stateMachine.Health.hitCount == 0)
        {
            stateMachine.ChangeState(new PlayerRangeState(stateMachine));
            return;
        }

        // Impact -> Groggy
        if (stateMachine.Health.hitCount == doubleHits)
        {
            stateMachine.ChangeState(new PlayerGroggyState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
    }
    #endregion
}
