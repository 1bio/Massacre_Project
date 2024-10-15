using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageSource : MonoBehaviour
{
    private Health _playerHealth;
    private Monster _monster;

    [SerializeField] private float _damageInterval = 1.0f;
    private bool _isPlayerInZone = false;

    private void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        _monster = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.IsEnableWeapon)
        {
            if (other.CompareTag("Player"))
            {
                if (_playerHealth == null)
                    return;

                _playerHealth.TakeDamage();
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerHealth != null)
            {
                _isPlayerInZone = true;
                StartCoroutine(DealDamageOverTime());
            }
        }
    }

    private void OnParticleExit(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInZone = false;
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (_isPlayerInZone)
        {
            if (_playerHealth != null)
            {
                _playerHealth.TakeDamage();
            }
            yield return new WaitForSeconds(_damageInterval);
        }
    }
}
