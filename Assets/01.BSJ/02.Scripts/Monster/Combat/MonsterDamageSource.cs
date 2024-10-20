using System.Collections;
using UnityEngine;

public class MonsterDamageSource : MonoBehaviour
{
    private Monster _monster;

    private bool _canTakeDamage = true;
    [SerializeField] private float _damageInterval = 1.5f;

    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.IsEnableWeapon)
        {
            if (other.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(20);
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()) && _canTakeDamage)
        {
            if(other.TryGetComponent<Health>(out Health health))
            {
                StartCoroutine(DealDamageOverTime(health));
            }
        }
    }

    private IEnumerator DealDamageOverTime(Health health)
    {
        _canTakeDamage = false;

        health.TakeDamage(_monster.MonsterSkillController.CurrentSkillData.Damage); 

        yield return new WaitForSeconds(_damageInterval);

        _canTakeDamage = true;
    }
}
