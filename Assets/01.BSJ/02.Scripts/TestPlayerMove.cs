using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    public float speed = 5f; // �ӵ� ����
    public int damage = 1;
    public Monster monster;

    void Update()
    {
        // �������� �̵�
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // ���������� �̵�
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // ���� �̵�
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        // �Ʒ��� �̵�
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (monster != null)
            {
                monster.TakeDamage(damage);
                Debug.Log(monster.MonsterAbility.MonsterHealth.CurrentHealth);
            }
        }
    }
}
