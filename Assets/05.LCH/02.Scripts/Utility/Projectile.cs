using UnityEngine;
using UnityEngine.Timeline;

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

            if (collision.gameObject.CompareTag("Projectile"))
                return;

            arrow_rigid.isKinematic = true; // 물리 연산 중지

            // 충돌 지점으로 위치 이동
            transform.position = collision.contacts[0].point;

            /*Vector3 dir = -transform.forward;
            transform.position = dir + collision.contacts[0].point;*/

            transform.SetParent(collision.transform); 

            health.TakeDamage();

            Debug.Log("Hit!");

            return;
        }

        // 벽이나 다른 오브젝트에 맞을 경우
        arrow_rigid.isKinematic = true; // 물리 연산 중지
    }
    #endregion
}

