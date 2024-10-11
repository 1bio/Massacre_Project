using UnityEngine;

public class MonsterCombatAbility : IMonsterCombat
{
    private readonly MonsterStatData _statData;

    public float MoveSpeed => _statData.MoveSpeed;
    public float TurnSpeed => _statData.TurnSpeed;

    public bool IsDead { get; set; }

    public MonsterSkillData MonsterSkillData { get; private set; }
    public MonsterHealth MonsterHealth { get; private set; }
    public MonsterAttack MonsterAttack { get; private set; }
    public MonsterTargetDistance MonsterTargetDistance { get; private set; }

    public MonsterCombatAbility(MonsterStatData statData)
    {
        _statData = statData;
        InitializeStats();
    }

    public MonsterCombatAbility(MonsterStatData statData, MonsterSkillData skillData)
    {
        _statData = statData;
        InitializeStats();

        MonsterSkillData = skillData;
    }

    private void InitializeStats()
    {
        MonsterHealth = new MonsterHealth(0, _statData.MonsterHealth.MaxHealth);
        MonsterHealth.InitializeHealth();

        // MonsterAttack 생성 시 필요한 모든 매개변수를 전달
        MonsterAttack = new MonsterAttack(
            _statData.MonsterAttack.Damage,
            _statData.MonsterAttack.CooldownThreshold,
            _statData.MonsterAttack.TotalCount,
            _statData.MonsterAttack.IsAttack,       // 공격 상태
            _statData.MonsterAttack.IsEnableWeapon   // 무기 활성화 상태
        );

        MonsterTargetDistance = new MonsterTargetDistance(
            _statData.MonsterTargetDistance.MinTargetDistance,
            _statData.MonsterTargetDistance.MaxTargetDistance,
            _statData.MonsterTargetDistance.IdealTargetDistance,
            _statData.MonsterTargetDistance.IdealTargetDistanceThreshold
        );
    }

    private void InitializeSkills()
    {

    }
}
