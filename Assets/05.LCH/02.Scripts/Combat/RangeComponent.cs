using UnityEngine;

public class RangeComponent : MonoBehaviour
{
    public Transform ShootingTransform;

    [SerializeField] private float projectileSpeed; // ����ü �ӵ�


    // �ִϸ��̼� �̺�Ʈ
    public void Shoot() // �Ϲ� ����
    {
        GameObject projectile = PoolManager.instance.Get(0);

        projectile.transform.position = ShootingTransform.transform.position;
        projectile.transform.rotation = ShootingTransform.transform.rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = ShootingTransform.transform.forward * projectileSpeed;
    }
}
