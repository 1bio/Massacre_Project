using System.Collections;
using UnityEngine;

public class MonsterDamageSource : MonoBehaviour
{
    private Monster _monster;
    // Health 주석 처리
    /*private PlayerHealth _playerHealth;*/

    private bool _canTakeDamage = true;
    [SerializeField] private float _damageInterval = 1.5f;

    private void Awake()
    {
        _monster = GetComponentInParent<Monster>();
        /*_playerHealth = FindObjectOfType<PlayerHealth>();*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.IsEnableWeapon &&
            other.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()))
        {
            if(other.TryGetComponent<Health>(out Health playerHealth))
            {
                playerHealth.TakeDamage(30, true); 
            }
            /*_playerHealth.TakeDamage(_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.Damage);*/
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()) && _canTakeDamage)
        {
            Debug.LogWarning(other.name);
            /*if (_playerHealth != null)
            {
                StartCoroutine(DealDamageOverTime(_playerHealth));
            }*/
        }
    }

    /*private IEnumerator DealDamageOverTime(Health health)
    {
        _canTakeDamage = false;

        health.TakeDamage(_monster.MonsterSkillController.CurrentSkillData.Damage);

        yield return new WaitForSeconds(_damageInterval);

        _canTakeDamage = true;
    }*/
}
