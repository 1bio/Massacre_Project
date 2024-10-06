using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageSource : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    // 플레이어 체력관련 클래스 가져오기   

    private Monster _monster; // SetAttack에 대한 참조

    private void Awake()
    {
        // 부모에서 SetAttack 컴포넌트를 가져오기
        _monster = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_monster.MonsterAbility.MonsterAttack.IsEnableWeapon)
        {
            if (other.CompareTag("Player"))
            {
                // 플레이어 체력 가져와서 감소
                Debug.Log("Player Hit");
            }
        }
    }
}
