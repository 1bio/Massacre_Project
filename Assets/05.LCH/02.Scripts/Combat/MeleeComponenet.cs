﻿using UnityEngine;

public class MeleeComponenet : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;

    private float damage;
    private float knockBack;

    // 공격력 및 넉백
    public void SetAttack(float damage, float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }


    #region Collision Methods
    private void OnTriggerEnter(Collider other) // 몬스터 피격 처리
    {
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;

            forceReceiver.AddForce(direction * knockBack);
        }

        if (other.TryGetComponent<CreatureHealth>(out CreatureHealth health))
        {
            if (other.CompareTag("Player"))
                return;

            cameraShake.ShakeCamera(1f, 0.2f);

            health?.TakeDamage(damage, false); // 공용 
        }
    }
    #endregion
}
