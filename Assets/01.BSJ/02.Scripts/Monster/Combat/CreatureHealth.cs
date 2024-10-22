using System;
using UnityEngine;

public class CreatureHealth : Health
{
    private Monster _monster;

    // �ǰ� ī��Ʈ �ʵ�
    public new int hitCount = 0;

    private float _currentTime;
    private float _lastImpactTime;

    private float _hitDurationCoolDown = 1f;

    private bool _isInvunerable;

    // �̺�Ʈ �ʵ�
    public new event Action ImpactEvent;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    public new void TakeDamage(float damage)
    {
        if (_isInvunerable)
            return;

        ImpactEvent.Invoke();

        float currentImpactTime = Time.time;
        _lastImpactTime = currentImpactTime;
        hitCount++;

        float health = _monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth;
        _monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth = Mathf.Max(health - damage, 0);
    }
}
