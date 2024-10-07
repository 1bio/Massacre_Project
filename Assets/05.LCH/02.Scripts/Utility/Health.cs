using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    // <==== 체력 필드 ====>
    private float currentHealth;
    private bool isDead => currentHealth <= 0;

    // <==== 피격 필드 ====>
    public int hitCount = 0;

    private float currentTime;
    private float lastImpactTime;

    private float coolDown = 1f;

    // <==== 피격 이벤트 ====>
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

        CheckCoolDown(); // 카운트 체크
    }


    #region Main Methods
    // 쿨타임 지날 시 카운트 초기화
    public void CheckCoolDown()
    {
        currentTime = Time.time;
        
        if (currentTime - lastImpactTime >= coolDown)
        {
            hitCount = 0;
        }
    }

    // 피격 당한 순간 lastImpactTime 업데이트 및 이벤트 호출
    public void TakeDamage() 
    {
        float currentImpactTime = Time.time;
        
        lastImpactTime = currentImpactTime;

        hitCount++; // hitCount 증가

        ImpactEvent?.Invoke();
    }

    public void Dead()
    {
        Debug.Log("Die!");
        // GameManager에서 이벤트 실행
    }
    #endregion
}
