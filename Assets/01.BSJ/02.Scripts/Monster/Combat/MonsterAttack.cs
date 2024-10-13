using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterAttack
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldownThreshold;
    [SerializeField] private int _totalCount;

    private bool _isAttack = false;
    private bool _isEnableWeapon = false;

    public MonsterAttack(int damage, float cooldownThreshold, int totalCount, bool isAttack, bool isEnableWeapon)
    {
        _damage = damage;
        _cooldownThreshold = cooldownThreshold;
        _totalCount = totalCount;
        _isAttack = isAttack;
        _isEnableWeapon = isEnableWeapon;
    }

    public int Damage { get => _damage; set => _damage = value; }
    public float CooldownThreshold { get => _cooldownThreshold; set => _cooldownThreshold = value; }
    public int TotalCount { get => _totalCount; set => _totalCount = value; }
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }
    public bool IsEnableWeapon { get => _isEnableWeapon; set => _isEnableWeapon = value; }
}
