public class MonsterCombatController
{
    public MonsterCombatController(MonsterStatData statData, Health health)
    {
        MonsterCombatAbility = new MonsterCombatAbility(statData);
        Health = health;
        Health.SetHealth(MonsterCombatAbility.MonsterHealth.MaxHealth);
        IsTargetInRange = false;

        MonsterCombatAbility.MonsterHealth.InitializeHealth();
    }

    public MonsterCombatController(MonsterStatData statData, MonsterSkillData skillData, Health health)
    {
        MonsterCombatAbility = new MonsterCombatAbility(statData, skillData);
        Health = health;
        Health.SetHealth(MonsterCombatAbility.MonsterHealth.MaxHealth);
        IsTargetInRange = false;

        MonsterCombatAbility.MonsterHealth.InitializeHealth();
    }

    public MonsterCombatAbility MonsterCombatAbility { get; private set; }
    public Health Health { get; private set; }
    public bool IsTargetInRange { get; set; }
}
