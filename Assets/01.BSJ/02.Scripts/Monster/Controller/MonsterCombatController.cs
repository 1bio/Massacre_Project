public class MonsterCombatController
{
    public MonsterCombatController(MonsterStatData statData, CreatureHealth health)
    {
        MonsterCombatAbility = new MonsterCombatAbility(statData);
        Health = health;

        IsTargetInRange = false;

        MonsterCombatAbility.MonsterHealth.InitializeHealth();
    }

    public MonsterCombatAbility MonsterCombatAbility { get; private set; }
    public CreatureHealth Health { get; private set; }
    public bool IsTargetInRange { get; set; }
}
