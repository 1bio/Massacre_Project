using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterHealth
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    public int Health
    {
        get => _health;
        set => _health = value;
    }
    public int MaxHealth => _maxHealth;
}
