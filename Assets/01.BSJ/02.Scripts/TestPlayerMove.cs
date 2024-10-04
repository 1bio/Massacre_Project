using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    public float speed = 5f; // 속도 설정

    void Update()
    {
        // 왼쪽으로 이동
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // 오른쪽으로 이동
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // 위로 이동
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        // 아래로 이동
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }
}
