public interface IMonsterCombat
{
    MonsterHealth MonsterHealth { get; }
    MonsterAttack MonsterAttack { get; }
    MonsterTargetDistance MonsterTargetDistance { get; }

    int MoveSpeed { get; }
    int TurnSpeed { get; }

    bool IsTurning { get; set; }
    bool IsDead { get; set; }    // 생사 여부
}

