using UnityEngine;

// 두 대 이상 연속 피격 당할 시(hitCount = 2) 그로기 상태로 전환
public class Health : MonoBehaviour
{
    public int hitCount = 0;

    private float currentTime;
    private float lastImpactTime;

    private float hitDurationCoolDown = 1f; // 1초 후에 hitCount 초기화

    private void Start()
    {
        lastImpactTime = hitDurationCoolDown;
    }

    private void Update()
    {
        CheckCoolDown(); 
    }


    #region Main Methods
    // 카운트 체크
    public void CheckCoolDown()
    {
        currentTime = Time.time;
        
        if (currentTime - lastImpactTime >= hitDurationCoolDown)
        {
            hitCount = 0;
        }
    }

    public virtual void TakeDamage(float damage) 
    {
        float currentImpactTime = Time.time;
        lastImpactTime = currentImpactTime;
        hitCount++; 
    }

    public virtual void Dead()
    {
    }
    #endregion
}
