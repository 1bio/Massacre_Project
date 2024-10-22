using System;
using UnityEngine;

public class PlayerHealth : Health
{
    private float health;

    public event Action ImpactEvent;

    private void Start()
    {
        health = DataManager.instance.playerData.statusData.currentHealth;    
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
       
        ImpactEvent?.Invoke();

        health = Mathf.Max(health - damage, 0f);

        DataManager.instance.playerData.statusData.currentHealth = health;

        Debug.Log($"Player health: {DataManager.instance.playerData.statusData.currentHealth}");

        if (health <= 0f)
        {
            Dead();
        }
    }

    public override void Dead()
    {
        base.Dead();

        Debug.Log("Player Die");

        // GameManager에서 이벤트 실행
    }
}
