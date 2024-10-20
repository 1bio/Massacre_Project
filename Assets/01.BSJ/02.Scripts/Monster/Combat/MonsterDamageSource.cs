using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageSource : MonoBehaviour
{
    private Health _playerHealth;
    private Monster _monster;

    private bool _canTakeDamage = true;
    [SerializeField] private float _damageInterval = 1.5f;

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

                _playerHealth.TakeDamage(20); // 데미지 가져와서 넣기
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()) && _canTakeDamage)
        {
            if (_playerHealth != null)
            {
                StartCoroutine(DealDamageOverTime());
            }
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        _canTakeDamage = false;

        _playerHealth.TakeDamage(_monster.MonsterSkillController.CurrentSkillData.Damage); // 데미지 가져와서 넣기
        Debug.Log("Player Hit");

        yield return new WaitForSeconds(_damageInterval);

        _canTakeDamage = true;
    }
}
