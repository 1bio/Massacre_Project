using UnityEngine;

/// <summary>
/// 발사 위치를 "ShootingTransform"으로 생성
/// </summary>

public class RangeWeaponComponent : MonoBehaviour
{
    public Transform ShootingTransform;

    [SerializeField] public float projectileSpeed; // 투사체 속도


    // 애니메이션 이벤트
    public void Shoot()
    {
        GameObject projectile = GameManager.Instance.PoolManager.Get(0);

        projectile.transform.position = ShootingTransform.transform.position;
        projectile.transform.rotation = ShootingTransform.transform.rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = ShootingTransform.transform.forward * projectileSpeed;
    }
}
