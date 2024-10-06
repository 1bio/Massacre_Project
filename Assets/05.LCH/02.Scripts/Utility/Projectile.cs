using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    private float damage;
    private float knockBack;

    private void OnEnable()
    {
        rigidbody.isKinematic = false;

        damage = DataManager.instance.playerData.rangeAttackData.Damage;
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = false;
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
        if (collision.gameObject.CompareTag("Projectile"))
            return;
     
        ContactPoint contact = collision.contacts[0]; // ù ��° �浹 ����

        Vector3 hitPoint = contact.point; // �浹 ��ġ
        Vector3 hitNormal = contact.normal; // �浹 ǥ�� ����

        // ȭ�� ��ġ �� ȸ�� ����
        transform.position = hitPoint;
        transform.rotation = Quaternion.LookRotation(hitNormal);

        transform.SetParent(collision.transform);

        rigidbody.isKinematic = true; // ���� ���� ����
    }
    #endregion
}
