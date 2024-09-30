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

        Debug.Log($"���� ü��: {currentHealth}");

        Dead();

        CountSetting();
    }


    #region Main Methods
    public void TakeDamage()
    {
        float currentTime = Time.time;

        hitCount++; // hitCount ����

        ImpactEvent?.Invoke();

        lastImpactTime = currentTime;
    }

    public void CountSetting()
    {
        float currentTime = Time.time;

        // ��Ÿ���� ������ ī��Ʈ�� �ʱ�ȭ
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
