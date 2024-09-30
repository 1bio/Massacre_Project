public interface IMonsterCombat
{
    int Health { get; set; } // 체력
    int MaxHealth { get; }  // 최대 체력
    int MoveSpeed { get; }
    int TurnSpeed { get; }

    // 공격 관련
    int AttackPower { get; } // 공격력
    float AttackCooldown { get; }   // 공격 쿨타임

    float MinTargetDistance { get; }    // 최소 거리
    float MaxTargetDistance { get; }    // 최대 거리
    float IdealTargetDistance { get; }  // 적정 거리
    float IdealTargetDistanceThreshold { get; } // 적정 거리 오차 범위


    bool IsDead { get; set; }    // 생사 여부
    bool IsAttacking { get; set; }   // 공격 중인지 확인
    bool IsStaggered { get; set; }   // 맞았는지 확인
    bool IsMoving { get; set; }  // 움직이는지 확인
}

