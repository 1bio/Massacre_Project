using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStat
{
    public MonsterAttack Attack { get; private set; }
    public MonsterHealth Health { get; private set; }
    public MonsterTargetDistance TargetDistance { get; private set; }

    // �����ڿ��� �Ű������� MonsterStatData�� ����
    public MonsterStat(MonsterStatData data)
    {
        Attack = new MonsterAttack(
            data.MonsterAttack.AttackPower,
            data.MonsterAttack.AttackCooldown,
            data.MonsterAttack.AttackTotalCount,
            data.MonsterAttack.IsAttack,
            data.MonsterAttack.IsEnableWeapon
        );

        Health = new MonsterHealth(
            0,  // ���� ü�� �ʱ�ȭ
            0,  // ������ ü�� �ʱ�ȭ
            data.MonsterHealth.MaxHealth,
            false // �⺻�� ����
        );

        // Health �ʱ�ȭ
        Health.InitializeHealth();

        TargetDistance = new MonsterTargetDistance(
            data.MonsterTargetDistance.MinTargetDistance,
            data.MonsterTargetDistance.MaxTargetDistance,
            data.MonsterTargetDistance.IdealTargetDistance,
            data.MonsterTargetDistance.IdealTargetDistanceThreshold
        );
    }
}


public class MonsterCombatAbility : IMonsterCombat
{
    private MonsterStatData _statData;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    [SerializeField] private bool _isDead = false;

    [SerializeField] private MonsterHealth _monsterHealth;
    [SerializeField] private MonsterAttack _monsterAttack;
    [SerializeField] private MonsterTargetDistance _monsterTargetDistance;

    public MonsterCombatAbility(MonsterStatData statData)
    {
        _statData = statData;

        // ������ ����
        _moveSpeed = statData.MoveSpeed;
        _turnSpeed = statData.TurnSpeed;

        _monsterHealth = new MonsterHealth(statData.MonsterHealth.CurrentHealth,
                                            statData.MonsterHealth.LastHealth,
                                            statData.MonsterHealth.MaxHealth,
                                            statData.MonsterHealth.IsHit);

        _monsterAttack = new MonsterAttack(statData.MonsterAttack.AttackPower,
                                            statData.MonsterAttack.AttackCooldown,
                                            statData.MonsterAttack.AttackTotalCount,
                                            statData.MonsterAttack.IsAttack,
                                            statData.MonsterAttack.IsEnableWeapon);

        _monsterTargetDistance = new MonsterTargetDistance(statData.MonsterTargetDistance.MinTargetDistance,
                                                            statData.MonsterTargetDistance.MaxTargetDistance,
                                                            statData.MonsterTargetDistance.IdealTargetDistance,
                                                            statData.MonsterTargetDistance.IdealTargetDistanceThreshold);
    }

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    public MonsterHealth MonsterHealth
    {
        get => _monsterHealth;
        set => _monsterHealth = value;
    }

    public MonsterAttack MonsterAttack {
        get => _monsterAttack;
        set => MonsterAttack = value; 
    }

    public MonsterTargetDistance MonsterTargetDistance
    {
        get => _monsterTargetDistance;
        set => _monsterTargetDistance = value;
    }
}
