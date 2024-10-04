using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterHealth
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _lastHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }
    public int LastHealth
    {
        get => _lastHealth;
        set => _lastHealth = value;
    }
}
