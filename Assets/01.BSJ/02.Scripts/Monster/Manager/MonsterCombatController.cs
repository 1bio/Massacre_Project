public class MonsterCombatController
{
    private MonsterCombatAbility _monsterCombatAbility;
    private Health _health;
    private bool _isTargetInRange;
    private float _basicAttackCoolTimeCheck;
    private float _skillAttackCoolTimeCheck;

    public MonsterCombatController(MonsterStatData statData, Health health)
    {
        _monsterCombatAbility = new MonsterCombatAbility(statData);
        _health = health;
        _isTargetInRange = false;
        _basicAttackCoolTimeCheck = 0f;
        _skillAttackCoolTimeCheck = 0f;
    }

    public MonsterCombatAbility MonsterCombatAbility => _monsterCombatAbility;
    public Health Health => _health;
    public bool IsTargetInRange { get => _isTargetInRange; set => _isTargetInRange = value; }
    public float BasicAttackCoolTimeCheck { get => _basicAttackCoolTimeCheck; set => _basicAttackCoolTimeCheck = value; }
    public float SkillAttackCoolTimeCheck { get => _skillAttackCoolTimeCheck; set => _skillAttackCoolTimeCheck = value; }
}
