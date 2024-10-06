using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Astar : MonoBehaviour
{
    public Transform TargetTransform => _targetTransform;
    public List<PointNode> Path => _path;
    public bool HasTargetMoved => _hasTargetMoved;


    private Monster _monster;
    private List<Monster> _monsters;
    private PointGrid _grid;

    private Transform _targetTransform;
    private Vector3 _lastTargetPos;

    private List<PointNode> _path;
    private bool _hasTargetMoved;
    private bool _isCalculating = false;

    [SerializeField] private float _separationRadius = 2f;


    private void Start()
    {
        _grid = FindObjectOfType<PointGrid>();
        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _monster = GetComponent<Monster>();
        _lastTargetPos = _targetTransform.position;

        _monsters = FindObjectsOfType<Monster>().ToList();

        StartPathCalculation();
    }

    private void Update()
    {
        if (_targetTransform != null)
        {
            OnHasTargetMoved();
        }

        if (_hasTargetMoved && !_isCalculating)
        {
            StartPathCalculation();
        }
    }


    public void StartPathCalculation()
    {
        StartCoroutine(CalculatePathCoroutine());
    }

    private IEnumerator CalculatePathCoroutine()
    {
        _isCalculating = true;
        _grid.InitializeNodeValues();
        _path = FindPath(this.transform.position, _targetTransform.position);
        _lastTargetPos = _targetTransform.position;
        _isCalculating = false;

        yield return null; // 다음 프레임으로 이동
    }

    private List<PointNode> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        PointNode startNode = _grid.GetPointNodeFromGridByPosition(startPos);
        PointNode targetNode = _grid.GetPointNodeFromGridByPosition(targetPos);

        if (startNode == null || targetNode == null)
        {
            return null;
        }

        List<PointNode> openList = new List<PointNode>();
        HashSet<PointNode> closedList = new HashSet<PointNode>(); // Closed List

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            PointNode currentNode = openList.OrderBy(node => node.FCost)
                                            .ThenBy(node => node.HCost)
                                            .FirstOrDefault();

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)
            {
                List<PointNode> path = new List<PointNode>();
                PointNode nextNode = targetNode;

                while (nextNode != null)
                {
                    path.Add(nextNode);

                    if (nextNode.Parent == null)
                        break;

                    nextNode = nextNode.Parent;
                }

                path.Reverse();
                return path;
            }

            // 주변 노드 생성
            List<PointNode> neighborNodes = _grid.GetNeighborNodes(currentNode);

            foreach (PointNode neighborNode in neighborNodes)
            {
                if (!neighborNode.IsGround || neighborNode.IsObstacle || closedList.Contains(neighborNode))
                    continue;

                float newMovementCost = currentNode.GCost + Vector3.Distance(neighborNode.Position, currentNode.Position);

                if (newMovementCost < neighborNode.GCost || !openList.Contains(neighborNode))
                {
                    neighborNode.GCost = newMovementCost;
                    neighborNode.HCost = Vector3.Distance(neighborNode.Position, targetNode.Position);

                    // 밀집도 계산
                    int nearbyMonsters = CalculateNearbyMonsters(neighborNode);
                    float congestionCost = nearbyMonsters * 2f;

                    // F = G + H + 밀집도
                    neighborNode.FCost = neighborNode.GCost + neighborNode.HCost + congestionCost;
                    neighborNode.Parent = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        return null;
    }

    private int CalculateNearbyMonsters(PointNode node)
    {
        int nearbyMonsters = 0;
        float totalDistanceFactor = 0f;

        foreach (Monster monster in _monsters)
        {
            float distance = Vector3.Distance(monster.transform.position, node.Position);
            if (distance < _separationRadius)
            {
                nearbyMonsters++;

                totalDistanceFactor += (_separationRadius - distance) / _separationRadius;
            }
        }

        return nearbyMonsters + Mathf.RoundToInt(totalDistanceFactor);
    }


    private void OnHasTargetMoved()
    {
        if (_lastTargetPos.x + _monster.MonsterAbility.MonsterTargetDistance.IdealTargetDistance < _targetTransform.position.x
            || _lastTargetPos.x - _monster.MonsterAbility.MonsterTargetDistance.IdealTargetDistance > _targetTransform.position.x
            || _lastTargetPos.z + _monster.MonsterAbility.MonsterTargetDistance.IdealTargetDistance < _targetTransform.position.z
            || _lastTargetPos.z - _monster.MonsterAbility.MonsterTargetDistance.IdealTargetDistance > _targetTransform.position.z)
            _hasTargetMoved = true;
        else
            _hasTargetMoved = false;
    }

    // OnDrawGizmos 메서드 추가하여 path 시각화
    private void OnDrawGizmos()
    {
        if (_path != null)
        {
            Gizmos.color = Color.yellow; // 경로를 노란색으로 표시

            for (int i = 0; i < _path.Count - 1; i++)
            {
                Gizmos.DrawLine(_path[i].Position, _path[i + 1].Position); // 노드 사이에 선 그리기
            }
        }
    }
}