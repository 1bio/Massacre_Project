using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatData", menuName = "Data/MonsterStatData")]
public class MonsterStatData : ScriptableObject, IMonsterCombat
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private MonsterHealth _monsterHealth;
    [SerializeField] private MonsterAttack _monsterAttack;
    [SerializeField] private MonsterTargetDistance _monsterTargetDistance;

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public bool IsDead { get; set; }
    public MonsterHealth MonsterHealth => _monsterHealth;
    public MonsterAttack MonsterAttack => _monsterAttack;
    public MonsterTargetDistance MonsterTargetDistance => _monsterTargetDistance;
}
