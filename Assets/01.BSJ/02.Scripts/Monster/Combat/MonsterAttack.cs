using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterAttack
{
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _attackTotalCount;

    private bool _isAttack = false;
    private bool _isEnableWeapon = false;

    public MonsterAttack(int attackPower, float attackCooldown, int attackTotalCount, bool isAttack, bool isEnableWeapon)
    {
        _attackPower = attackPower;
        _attackCooldown = attackCooldown;
        _attackTotalCount = attackTotalCount;
        _isAttack = isAttack;
        _isEnableWeapon = isEnableWeapon;
    }

    public int AttackPower { get => _attackPower; set => _attackPower = value; }
    public float AttackCooldown { get => _attackCooldown; set => _attackCooldown = value; }
    public int AttackTotalCount { get => _attackTotalCount; set => _attackTotalCount = value; }
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }
    public bool IsEnableWeapon { get => _isEnableWeapon; set => _isEnableWeapon = value; }
}
