using UnityEngine;

[System.Serializable]
public class MonsterHealth
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;

    public MonsterHealth(int currentHealth, int maxHealth)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
    }

    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public void InitializeHealth()
    {
        _currentHealth = _maxHealth;
    }
}
