using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    // Monster Spawn
    private float _currentTime;
    [SerializeField] private int _spawnLimit;
    [SerializeField] private float _spawnCooldown; // `int`에서 `float`로 변경하여 더 세밀한 조정 가능

    // Total Monster Count
    [SerializeField] private int _monsterCount;

    // Monster Wave
    private int _currentWave;
    [SerializeField] private GameObject[] _firstWaveMonsters;
    [SerializeField] private GameObject[] _secondWaveMonsters;
    [SerializeField] private GameObject[] _thirdWaveMonsters;
    private Dictionary<int, GameObject[]> _monsterWaves;

    private void Awake()
    {
        _currentTime = 0;
        _currentWave = 1;

        _monsterWaves = new Dictionary<int, GameObject[]>()
        {
            { 1, _firstWaveMonsters },
            { 2, _secondWaveMonsters },
            { 3, _thirdWaveMonsters }
        };
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _spawnCooldown && IsMonsterCountWithinSpawnLimit())
        {
            SpawnMonsterWave();
            _currentTime = 0f;
        }
    }

    private void SpawnMonsterWave()
    {
        if (_monsterWaves.ContainsKey(_currentWave) && _monsterWaves[_currentWave].Length > 0)
        {
            OnSetActiveMonsters();
        }
        _currentWave++;
    }

    private void OnSetActiveMonsters()
    {
        foreach (GameObject monster in _monsterWaves[_currentWave])
        {
            if (monster != null)
            {
                monster.SetActive(true);
                _monsterCount++;
            }
        }
    }

    private bool IsMonsterCountWithinSpawnLimit() => _monsterCount <= _spawnLimit;
}
