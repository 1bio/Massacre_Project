using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponComponent : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private float damage;
    private float knockBack;


    // 공격력 및 넉백
    public void SetAttack(float damage, float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }


    #region Collision Methods
    private void OnTriggerEnter(Collider other) // 몬스터 피격 시 처리
    {
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;

            forceReceiver.AddForce(direction * knockBack);
        }

        if (other.TryGetComponent<Health>(out Health health))
        {
            if (other == myCollider)
                return;

            Debug.Log("Hit!");

            health?.TakeDamage();
        }
    }
    #endregion
}
