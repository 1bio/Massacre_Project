using UnityEngine;

public class RangeWeaponComponent : MonoBehaviour
{
    public Transform ShootingTransform;

    [SerializeField] private float projectileSpeed; // 투사체 속도
  
    private float damage;
    private float knockBack;


    // 공격력 및 넉백
    public void SetAttack(float damage, float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }

    // 애니메이션 이벤트
    public void Shoot()
    {
        GameObject projectile = DataManager.instance.PoolManager.Get(0);

        projectile.transform.position = ShootingTransform.transform.position;
        projectile.transform.rotation = ShootingTransform.transform.rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = ShootingTransform.transform.forward * projectileSpeed;
    }
}
