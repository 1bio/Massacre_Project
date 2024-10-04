using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterAttack
{
    [SerializeField] private int _attackPower;
    [SerializeField] private float _attackCooldown;
    private bool _isAttack = false;

    public int AttackPower => _attackPower;
    public float AttackCooldown => _attackCooldown;
    public bool IsAttack
    {
        get => _isAttack;
        set => _isAttack = value;
    }
}
