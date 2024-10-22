using UnityEngine;

public class MonsterCombatAbility : IMonsterCombat
{
    private readonly MonsterStatData _statData;

    public float MoveSpeed => _statData.MoveSpeed;
    public float TurnSpeed => _statData.TurnSpeed;

    public bool IsDead { get; set; }

    public MonsterHealth MonsterHealth { get; private set; }
    public MonsterAttack MonsterAttack { get; private set; }
    public MonsterTargetDistance MonsterTargetDistance { get; private set; }

    public MonsterCombatAbility(MonsterStatData statData)
    {
        _statData = statData;
        InitializeStats();
    }

    private void InitializeStats()
    {
        MonsterHealth = new MonsterHealth(0, _statData.MonsterHealth.MaxHealth);
        MonsterHealth.InitializeHealth();

        MonsterAttack = new MonsterAttack(
            _statData.MonsterAttack.Damage,
            _statData.MonsterAttack.Range,
            _statData.MonsterAttack.CooldownThreshold,
            _statData.MonsterAttack.TotalCount,
            _statData.MonsterAttack.IsTargetWithinAttackRange,
            _statData.MonsterAttack.IsEnableWeapon 
        );
    }
}
