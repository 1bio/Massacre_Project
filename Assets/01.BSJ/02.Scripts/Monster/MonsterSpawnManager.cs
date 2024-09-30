using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    // Monster Spawn
    private float currentTime;
    [SerializeField] private int spawnLimit;
    [SerializeField] private int spawnCooldown;
    

    // Total Monster Count
    [SerializeField] private int monsterCount;


    // Monster Wave
    private int currentWave;
    [SerializeField] private GameObject[] firstWaveMonsters;
    [SerializeField] private GameObject[] secondWaveMonsters;
    [SerializeField] private GameObject[] thirdWaveMonsters;
    private Dictionary<int, GameObject[]> monsterWaves;


    private void Awake()
    {
        currentTime = 0;
        currentWave = 1;

        monsterWaves = new Dictionary<int, GameObject[]>()
        {
            { 1, firstWaveMonsters },
            { 2, secondWaveMonsters },
            { 3, thirdWaveMonsters }
        };
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= spawnCooldown && IsMonsterCountWithinSpawnLimit())
        {
            SpawnMonsterWave();
            currentTime = 0f;
        }
    }

    private void SpawnMonsterWave()
    {
        if (monsterWaves.ContainsKey(currentWave) && monsterWaves[currentWave].Length > 0)
        {
            OnSetActiveMonsters();
        }
        currentWave++;
    }

    private void OnSetActiveMonsters()
    {
        foreach (GameObject monster in monsterWaves[currentWave])
        {
            if (monster != null)
            {
                monster.SetActive(true);
                monsterCount++;
            }
        }
    }

    private bool IsMonsterCountWithinSpawnLimit()
    {
        if (monsterCount <= spawnLimit)
        {
            return true;
        }

        return false;
    }
}
