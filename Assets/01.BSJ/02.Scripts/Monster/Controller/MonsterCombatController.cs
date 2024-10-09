public class MonsterCombatController
{
    public MonsterCombatController(MonsterStatData statData, Health health)
    {
        MonsterCombatAbility = new MonsterCombatAbility(statData);
        Health = health;
        IsTargetInRange = false;
        BasicAttackCoolTimeCheck = 0f;
        SkillAttackCoolTimeCheck = 0f;
    }

    public MonsterCombatAbility MonsterCombatAbility { get; private set; }
    public Health Health { get; private set; }
    public bool IsTargetInRange { get; set; }
    public float BasicAttackCoolTimeCheck { get; set; }
    public float SkillAttackCoolTimeCheck { get; set; }
}
