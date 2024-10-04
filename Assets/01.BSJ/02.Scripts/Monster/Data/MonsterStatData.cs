using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatData", menuName = "Data/MonsterStatData")]
public class MonsterStatData : ScriptableObject, IMonsterCombat
{
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _turnSpeed;

    [SerializeField] private MonsterHealth _monsterHealth;
    [SerializeField] private MonsterAttack _monsterAttack;
    [SerializeField] private MonsterTargetDistance _monsterTargetDistance;

    public int MoveSpeed => _moveSpeed;
    public int TurnSpeed => _turnSpeed;

    public bool IsDead { get; set; }

    public MonsterHealth MonsterHealth { get => _monsterHealth; }
    public MonsterAttack MonsterAttack { get => _monsterAttack; }
    public MonsterTargetDistance MonsterTargetDistance { get => _monsterTargetDistance; }
}
