using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterHealth
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _lastHealth;
    [SerializeField] private int _maxHealth;

    private bool _isHit = false;

    public MonsterHealth(int currentHealth, int lastHealth, int maxHealth, bool isHit)
    {
        _currentHealth = currentHealth;
        _lastHealth = lastHealth;
        _maxHealth = maxHealth;
        _isHit = isHit;
    }

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
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public bool IsHit
    {
        get => _isHit;
        set => _isHit = value;
    }


    public void InitializeHealth()
    {
        _currentHealth = _maxHealth;
        _lastHealth = _currentHealth;
    }
}
