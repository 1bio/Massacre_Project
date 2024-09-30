using UnityEngine;

/// <summary>
/// �߻� ��ġ�� "ShootingTransform"���� ����
/// </summary>

public class RangeWeaponComponent : MonoBehaviour
{
    public Transform ShootingTransform;

    [SerializeField] public float projectileSpeed; // ����ü �ӵ�


    // �ִϸ��̼� �̺�Ʈ
    public void Shoot()
    {
        GameObject projectile = GameManager.Instance.PoolManager.Get(0);

        projectile.transform.position = ShootingTransform.transform.position;
        projectile.transform.rotation = ShootingTransform.transform.rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = ShootingTransform.transform.forward * projectileSpeed;
    }
}
