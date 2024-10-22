using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float currentHealth;

    // 피격 카운트 필드
    public int hitCount = 0;

    private float currentTime;
    private float lastImpactTime;

    private float hitDurationCoolDown = 1f; // 1초 후에 hitCount 초기화

    private bool isInvunerable;

    // 이벤트 필드
    public event Action ImpactEvent;
    // Groggy 이벤트
    // 사망 이벤트

    private void Start()
    {
        lastImpactTime = hitDurationCoolDown;
    }

    private void Update()
    {
        CheckCoolDown(); 
    }

    #region Main Methods
    public void SetHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
    }

    public void SetInvulnerable(bool isInvunerable) // 무적 상태 체크(플레이어 로직[PlayerRollingState])
    {
        this.isInvunerable = isInvunerable;
    }

    public void CheckCoolDown() // 카운트 체크
    {
        currentTime = Time.time;
        
        if (currentTime - lastImpactTime >= hitDurationCoolDown)
        {
            hitCount = 0;
        }
    }

    public void TakeDamage(float damage) 
    {
        if (isInvunerable)
            return;

        ImpactEvent.Invoke();

        // 피격 횟수 로직
        float currentImpactTime = Time.time;
        lastImpactTime = currentImpactTime;
        hitCount++;

        // 데미지 처리 로직
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        
        if(currentHealth == 0)
        {
            Dead();
        }

        DataManager.instance.playerData.statusData.currentHealth = currentHealth;
    }


    public void Dead()
    {

    }
    #endregion
}
