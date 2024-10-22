using System;
using UnityEngine;

public class CreatureHealth : Health
{
    private Monster monster;

    public event Action ImpactEvent;

    private void Start()
    {
        monster = GetComponent<Monster>();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        ImpactEvent?.Invoke();

        float health = monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth;

        monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth = Mathf.Max(health - damage, 0);

        Debug.Log($"Creature health: {health}");
    }
}
