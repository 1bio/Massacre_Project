using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCombatAbility : IMonsterCombat
{
    private MonsterStatData _statData;

    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _turnSpeed;

    [SerializeField] private bool _isTurning = false;
    [SerializeField] private bool _isDead = false;

    [SerializeField] private MonsterHealth _monsterHealth;
    [SerializeField] private MonsterAttack _monsterAttack;
    [SerializeField] private MonsterTargetDistance _monsterTargetDistance;

    public MonsterCombatAbility(MonsterStatData statData)
    {
        _statData = statData;

        _moveSpeed = statData.MoveSpeed;
        _turnSpeed = statData.TurnSpeed;

        _monsterHealth = statData.MonsterHealth;
        _monsterAttack = statData.MonsterAttack;
        _monsterTargetDistance = statData.MonsterTargetDistance;
    }

    public int MoveSpeed => _moveSpeed;
    public int TurnSpeed => _turnSpeed;

    public bool IsTurning
    {
        get => _isTurning;
        set => _isTurning = value;
    }
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
