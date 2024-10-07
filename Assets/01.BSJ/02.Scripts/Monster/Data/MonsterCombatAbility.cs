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
        MonsterHealth = new MonsterHealth(0, 0, _statData.MonsterHealth.MaxHealth, false);
        MonsterHealth.InitializeHealth();

        // MonsterAttack ���� �� �ʿ��� ��� �Ű������� ����
        MonsterAttack = new MonsterAttack(
            _statData.MonsterAttack.AttackPower,
            _statData.MonsterAttack.AttackCooldown,
            _statData.MonsterAttack.AttackTotalCount,
            _statData.MonsterAttack.IsAttack,       // ���� ����
            _statData.MonsterAttack.IsEnableWeapon   // ���� Ȱ��ȭ ����
        );

        MonsterTargetDistance = new MonsterTargetDistance(
            _statData.MonsterTargetDistance.MinTargetDistance,
            _statData.MonsterTargetDistance.MaxTargetDistance,
            _statData.MonsterTargetDistance.IdealTargetDistance,
            _statData.MonsterTargetDistance.IdealTargetDistanceThreshold
        );
    }
}
