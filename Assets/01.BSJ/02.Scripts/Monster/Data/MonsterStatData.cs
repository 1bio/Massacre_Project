using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatData", menuName = "Data/MonsterStatData")]
public class MonsterStatData : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int moveSpeed;
    public int turnSpeed;
    public int attackPower;
    public float attackCooldown;
    public float minTargetDistance;
    public float maxTargetDistance;
    public float idealTargetDistance;
    public float idealTargetDistanceThreshold;
}