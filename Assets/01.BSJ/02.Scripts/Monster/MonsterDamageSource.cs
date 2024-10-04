using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageSource : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    // �÷��̾� ü�°��� Ŭ���� ��������   

    private Monster _monster; // SetAttack�� ���� ����

    private void Awake()
    {
        // �θ𿡼� SetAttack ������Ʈ�� ��������
        _monster = GetComponentInParent<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_monster.MonsterAbility.MonsterAttack.IsEnableWeapon)
        {
            if (other.CompareTag("Player"))
            {
                // �÷��̾� ü�� �����ͼ� ����
                Debug.Log("Player Hit");
            }
        }
    }
}
