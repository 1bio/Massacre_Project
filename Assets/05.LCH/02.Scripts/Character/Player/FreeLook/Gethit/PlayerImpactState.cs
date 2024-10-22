using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
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
        stateMachine.Animator.CrossFadeInFixedTime(Impact, CrossFadeDuration);
    }


    public override void Tick(float deltaTime)
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        /*// Impact -> Groggy
        if (stateMachine.Health.hitCount == doubleHits)
        {
            stateMachine.ChangeState(new PlayerGroggyState(stateMachine));
            return;
        }*/

        // FreeLook
        if (currentInfo.normalizedTime >= 0.8f && stateMachine.WeaponPrefabs[0].activeSelf)
        {
            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }
        else if (currentInfo.normalizedTime >= 0.8f && stateMachine.WeaponPrefabs[1].activeSelf)
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
