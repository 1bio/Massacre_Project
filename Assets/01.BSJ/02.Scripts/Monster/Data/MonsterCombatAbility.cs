using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCombatAbility : IMonsterCombat
{
    private MonsterStatData statData;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int moveSpeed;
    [SerializeField] private int turnSpeed;

    [SerializeField] private int attackPower;
    [SerializeField] private float attackCooldown;

    [SerializeField] private float minTargetDistance;
    [SerializeField] private float maxTargetDistance;
    [SerializeField] private float idealTargetDistance;
    [SerializeField] private float idealTargetDistanceThreshold;

    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isStaggered = false;
    [SerializeField] private bool isMoving = false;


    public MonsterCombatAbility(MonsterStatData statData)
    {
        this.statData = statData;

        health = statData.maxHealth;
        maxHealth = statData.maxHealth;
        moveSpeed = statData.moveSpeed;
        turnSpeed = statData.turnSpeed;

        attackPower = statData.attackPower;
        attackCooldown = statData.attackCooldown;

        minTargetDistance = statData.minTargetDistance;
        maxTargetDistance = statData.maxTargetDistance;
        idealTargetDistance = statData.idealTargetDistance;
        idealTargetDistanceThreshold = statData.idealTargetDistanceThreshold;
    }


    public int Health
    {
        get => health;
        set => health = value;
    }
    public int MaxHealth => maxHealth;
    public int MoveSpeed => moveSpeed;
    public int TurnSpeed => turnSpeed;
    public int AttackPower => attackPower;
    public float AttackCooldown => attackCooldown;

    public float MinTargetDistance => minTargetDistance;
    public float MaxTargetDistance => maxTargetDistance;
    public float IdealTargetDistance => idealTargetDistance;
    public float IdealTargetDistanceThreshold => idealTargetDistanceThreshold;

    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }

    public bool IsAttacking
    {
        get => isAttacking;
        set => isAttacking = value;
    }

    public bool IsStaggered
    {
        get => isStaggered;
        set => isStaggered = value;
    }

    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }
}
