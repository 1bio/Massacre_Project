using System;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float currentHealth;

    // 피격 카운트 필드
    private int hitCount;
    private int groggyCount = 3;

    private float currentTime;
    private float lastImpactTime;

    private float hitDurationCoolDown = 1f; // 1초 후에 hitCount 초기화

    private bool isInvunerable;

    // 이벤트 필드
    public event Action ImpactEvent;
    public event Action GroggyEvent;
    public event Action DeadEvent;

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

    public void SetInvulnerable(bool isInvunerable) // 무적 상태 적용
    {
        this.isInvunerable = isInvunerable;
    }

    private void CheckCoolDown() // 카운트 체크
    {
        currentTime = Time.time;
        
        if (currentTime - lastImpactTime >= hitDurationCoolDown)
        {
            hitCount = 0;
        }
    }

    public void TakeDamage(float damage, bool IsPlayer)
    {
        if (isInvunerable)
            return;

        ImpactEvent?.Invoke();

        Groggy(); 

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (currentHealth == 0)
        {
            Dead();
        }

        // 플레이어 체력 초기화
        if (IsPlayer)
        {
            DataManager.instance.playerData.statusData.currentHealth = currentHealth;
            return;
        }
        else // 몬스터 체력 초기화
        {
            Monster monster = GetComponent<Monster>();
            currentHealth = Mathf.Max(monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth - damage, 0);
            monster.MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth = currentHealth;
            if (!monster.MovementController.TargetDetector.IsTargetDetected)
                monster.MovementController.TargetDetector.IsTargetDetected = true;
        }
    }

    // hitCount 확인
    private void Groggy()
    {
        // 피격 횟수 로직
        float currentImpactTime = Time.time;
        lastImpactTime = currentImpactTime;
        hitCount++;

        if (hitCount >= groggyCount)
        {
            GroggyEvent?.Invoke();

            hitCount = 0;
        }
    }

    private void Dead()
    {
        DeadEvent?.Invoke();

        // GameManager에서 이벤트 실행, 로비로 가기, UI 패널 열기 등 
    }
    #endregion
}
