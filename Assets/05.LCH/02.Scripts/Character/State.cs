using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    public abstract void Enter(); // 상태 진입 시
    public abstract void Tick(float deltaTime); // 매 프레임마다 수행
    public abstract void Exit(); // 상태 종료 시

    // 공격 애니메이션 normalizedTime 값 리턴
    protected float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            // 전환 중 일때 실행되는 부분
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            // 애니메이션 재생 중 실행되는 부분
            return currentInfo.normalizedTime;
        }
        else
        {
            // 애니메이션 재생 종료 후 실행되는 부분
            return 0f;
        }
    }
}
