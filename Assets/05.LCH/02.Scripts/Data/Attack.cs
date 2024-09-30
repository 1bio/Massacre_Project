using UnityEngine;
using System;

[Serializable]
public class Attack 
{
    [field: SerializeField] public string AnimationName { get; private set; } // 애니메이션 이름

    [field: SerializeField] public int ComboAttackIndex { get; set; } = -1; // 콤보 애니메이션 인덱스

    [field: SerializeField] public float ComboAttackTime { get; private set; } // 콤보 공격 시전 시간

    [field: SerializeField] public float TransitionDuration { get; private set; } // 애니메이션 전환 시가 

    [field: SerializeField] public float Force { get; private set; } // 공격 시 이동 하는 속도

    [field: SerializeField] public float ForceTime { get; private set; } // 시전 시간

    [field: SerializeField] public float Damage { get; private set; } // 데미지

    [field: SerializeField] public float KnockBack { get; private set; } // 넉백 속도

}
