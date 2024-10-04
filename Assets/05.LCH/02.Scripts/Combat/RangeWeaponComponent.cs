using UnityEngine;

public class RangeWeaponComponent : MonoBehaviour
{
    public Transform ShootingTransform;

    [SerializeField] private float projectileSpeed; // ����ü �ӵ�
  
    private float damage;
    private float knockBack;


    // ���ݷ� �� �˹�
    public void SetAttack(float damage, float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }

    // �ִϸ��̼� �̺�Ʈ
    public void Shoot()
    {
        GameObject projectile = DataManager.instance.PoolManager.Get(0);

        projectile.transform.position = ShootingTransform.transform.position;
        projectile.transform.rotation = ShootingTransform.transform.rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = ShootingTransform.transform.forward * projectileSpeed;
    }
}
