using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    ParticleSystem particleSystem = new ParticleSystem();
    [SerializeField] private float _moveSpeed;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particleSystem.isPlaying)
        {
            Vector3 moveDirection = transform.forward;
            particleSystem.transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        }
    }
}
