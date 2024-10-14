using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody arrow_rigid;

    private float damage;
    private float knockBack;

    private void OnEnable()
    {
        damage = DataManager.instance.playerData.rangeAttackData.Damage;

        arrow_rigid = GetComponent<Rigidbody>();
        arrow_rigid.isKinematic = false;
    }


    #region Collision Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            if (collision.gameObject.CompareTag("Player"))
                return;

            health?.TakeDamage();

            Debug.Log("Hit!");
        }

        // Arrow Stick
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (collision.gameObject.CompareTag("Projectile"))
                return;

            arrow_rigid.isKinematic = true; // 물리 연산 중지

            ContactPoint contact = collision.contacts[0]; // 첫 번째 충돌 지점

            Vector3 hitPoint = contact.point; // 충돌 위치
            Vector3 hitNormal = contact.normal; // 충돌 표면 법선

            // 화살 위치 및 회전 조정
            transform.position = hitPoint;
            transform.rotation = Quaternion.LookRotation(-hitNormal);

            transform.SetParent(collision.transform);
            return;
        }

        // 안맞을 경우 물리 연산 끄기(임시)
        arrow_rigid.isKinematic = true; // 물리 연산 중지
    }
    #endregion
}

