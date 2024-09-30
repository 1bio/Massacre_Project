using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int hitCount = 0;
        
    private bool isDead => currentHealth <= 0;

    private float coolTime = 1f;
    private float lastImpactTime;

    public event Action ImpactEvent;


    private void Awake()
    {
        currentHealth = maxHealth;
        lastImpactTime = coolTime; 
    }

    private void Update()
    {
        if (!isDead)
            return;

        Debug.Log($"현재 체력: {currentHealth}");

        Dead();

        CountSetting();
    }


    #region Main Methods
    public void TakeDamage()
    {
        float currentTime = Time.time;

        hitCount++; // hitCount 증가

        ImpactEvent?.Invoke();

        lastImpactTime = currentTime;
    }

    public void CountSetting()
    {
        float currentTime = Time.time;

        // 쿨타임이 지나면 카운트를 초기화
        if (currentTime - lastImpactTime >= coolTime)
        {
            hitCount = 0;
        }
    }

    public void Dead()
    {
        if (isDead)
        {
            Debug.Log("Die!");
        }
    }
    #endregion
}
