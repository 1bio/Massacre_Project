using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float time; 

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        time = Time.time;
    }

    public float ReturnGameTime()
    {
        return time;
    }
}
