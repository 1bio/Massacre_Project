using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    // Monster Wave
    [SerializeField] private int _currentWave;
    [SerializeField] private GameObject[] _firstWaveMonsters;
    [SerializeField] private GameObject[] _secondWaveMonsters;
    [SerializeField] private GameObject[] _thirdWaveMonsters;
    private GameObject[][] _monsterWaves;
    private int _waveCount = 3;

    // Spawn Point
    [SerializeField] private GameObject _player;
    private PointGrid _pointGrid;
    private HashSet<PointNode> _neighborNodes = new HashSet<PointNode>();

    private bool _isFirstSpawn = true;


    private void Awake()
    {
        _currentWave = 0;

        _monsterWaves = new GameObject[][] { _firstWaveMonsters, _secondWaveMonsters, _thirdWaveMonsters };

        _pointGrid = FindObjectOfType<PointGrid>();
    }

    private void Start()
    {
        foreach (GameObject[] wave in _monsterWaves)
        {
            foreach (GameObject monster in wave)
            {
                monster.SetActive(false);
            }
        }
    }


    private void Update()
    {
        if (!_isFirstSpawn && IsWaveCleared(_monsterWaves[_currentWave]))
            _currentWave++;

        SpawnMonsterWave();

        _isFirstSpawn = false;
    }

    private void SpawnMonsterWave()
    {
        if (_monsterWaves[_currentWave].Length > 0 && IsWaveCleared(_monsterWaves[_currentWave]))
        {
            OnSetActiveMonsters();
        }
    }

    private void OnSetActiveMonsters()
    {
        foreach (GameObject monster in _monsterWaves[_currentWave])
        {
            if (monster != null)
            {
                monster.SetActive(true);
                monster.transform.position = FindRandomSpawnPoint();
            }
        }
    }

    private bool IsWaveCleared(GameObject[] monsterWave)
    {
        foreach (GameObject monster in monsterWave)
        {
            if (monster != null && monster.activeSelf)
                return false;
        }
        return true;
    }

    private Vector3 FindRandomSpawnPoint()
    {
        if (_player == null)
            return Vector3.zero;

        _neighborNodes.Clear();

        List<PointNode> nodes = new List<PointNode>();

        PointNode currentNode = _pointGrid.GetPointNodeFromGridByPosition(_player.transform.position);

        nodes.AddRange(_pointGrid.GetNeighborNodes(currentNode));

        foreach (PointNode node in nodes)
        {
            _neighborNodes.Add(node);

            foreach (PointNode neighborNode in _pointGrid.GetNeighborNodes(node))
            {
                _neighborNodes.Add(neighborNode);

                foreach (PointNode neighborNodeOfNeighbor in _pointGrid.GetNeighborNodes(neighborNode))
                {
                    _neighborNodes.Add(neighborNodeOfNeighbor);
                }
            }
        }

        foreach (PointNode node in nodes)
        {
            _neighborNodes.Remove(node);
        }

        int randNum = Random.Range(0, _neighborNodes.Count);

        PointNode[] neighborArray = new PointNode[_neighborNodes.Count];
        _neighborNodes.CopyTo(neighborArray);

        return neighborArray[randNum].Position;
    }
}
