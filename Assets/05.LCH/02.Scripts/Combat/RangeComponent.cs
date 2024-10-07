using UnityEngine;

public class RangeComponent : MonoBehaviour
{
    public Transform ShootingTransform;

    [SerializeField] private float projectileSpeed; // 투사체 속도


    // 애니메이션 이벤트
    public void Shoot() // 일반 공격
    {
        GameObject projectile = PoolManager.instance.Get(0);

        projectile.transform.position = ShootingTransform.transform.position;
        projectile.transform.rotation = ShootingTransform.transform.rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = ShootingTransform.transform.forward * projectileSpeed;
    }
}
