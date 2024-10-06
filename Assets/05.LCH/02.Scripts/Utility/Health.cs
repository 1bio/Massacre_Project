using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    // <==== ü�� �ʵ� ====>
    private float currentHealth;
    private bool isDead => currentHealth <= 0;

    // <==== �ǰ� �ʵ� ====>
    public int hitCount = 0;

    private float currentTime;
    private float lastImpactTime;

    private float coolDown = 1f;

    // <==== �ǰ� �̺�Ʈ ====>
    public event Action ImpactEvent;


    private void Start()
    {
        currentHealth = DataManager.instance.playerData.statusData.maxHealth;

        lastImpactTime = coolDown;
    }

    private void Update()
    {
        if (isDead)
        {
            Dead();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
        }

        CheckCoolDown(); // ī��Ʈ üũ
    }


    #region Main Methods
    // ��Ÿ�� ���� �� ī��Ʈ �ʱ�ȭ
    public void CheckCoolDown()
    {
        currentTime = Time.time;
        
        if (currentTime - lastImpactTime >= coolDown)
        {
            hitCount = 0;
        }
    }

    // �ǰ� ���� ���� lastImpactTime ������Ʈ �� �̺�Ʈ ȣ��
    public void TakeDamage() 
    {
        float currentImpactTime = Time.time;
        
        lastImpactTime = currentImpactTime;

        hitCount++; // hitCount ����

        ImpactEvent?.Invoke();
    }

    public void Dead()
    {
        Debug.Log("Die!");
        // GameManager���� �̺�Ʈ ����
    }
    #endregion
}
