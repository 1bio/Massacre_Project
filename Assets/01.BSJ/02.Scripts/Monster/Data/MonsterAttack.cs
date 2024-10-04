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

    public int AttackPower => _attackPower;
    public float AttackCooldown => _attackCooldown;
    public int AttackTotalCount => _attackTotalCount;

    public bool IsAttack
    {
        get => _isAttack;
        set => _isAttack = value;
    }
    public bool IsEnableWeapon
    {
        get => _isEnableWeapon;
        set => _isEnableWeapon = value;
    }
}
