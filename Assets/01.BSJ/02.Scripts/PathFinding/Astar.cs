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
    private PointGrid _grid;

    private Transform _targetTransform;
    private Vector3 _lastTargetPos;

    [SerializeField] private List<PointNode> _path;
    private bool _hasTargetMoved;
    private bool _isCalculating = false;

    // FindPath ȣ���� �Ÿ�

    private void Awake()
    {
        _grid = FindObjectOfType<PointGrid>();
        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _monster = GetComponent<Monster>();
        _lastTargetPos = _targetTransform.position;
    }

    private void Start()
    {
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

        yield return null; // ���� ���������� �̵�
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

            // �ֺ� ��� ����
            List<PointNode> neighborNodes = _grid.GetNeighborNodes(currentNode);

            foreach (PointNode neighborNode in neighborNodes)
            {
                if (!neighborNode.IsGround || neighborNode.IsObstacle || closedList.Contains(neighborNode))
                    continue;

                if (openList.Contains(neighborNode) && neighborNode.GCost > currentNode.GCost)
                    continue;

                neighborNode.GCost = currentNode.GCost + Vector3.Distance(neighborNode.Position, currentNode.Position);
                neighborNode.HCost = Vector3.Distance(neighborNode.Position, targetNode.Position);
                neighborNode.FCost = neighborNode.GCost + neighborNode.HCost;
                neighborNode.Parent = currentNode;

                if (!openList.Contains(neighborNode))
                {
                    openList.Add(neighborNode);
                }
            }
        }

        // ����, ã�� �������� null�� ��ȯ��
        return null;
    }

    private void OnHasTargetMoved()
    {
        if (_lastTargetPos.x + _monster.MonsterAbility.MonsterTargetDistance.MaxTargetDistance < _targetTransform.position.x
            || _lastTargetPos.x - _monster.MonsterAbility.MonsterTargetDistance.MaxTargetDistance > _targetTransform.position.x
            || _lastTargetPos.z + _monster.MonsterAbility.MonsterTargetDistance.MaxTargetDistance < _targetTransform.position.z
            || _lastTargetPos.z - _monster.MonsterAbility.MonsterTargetDistance.MaxTargetDistance > _targetTransform.position.z)
            _hasTargetMoved = true;
        else
            _hasTargetMoved = false;
    }

    // OnDrawGizmos �޼��� �߰��Ͽ� path �ð�ȭ
    private void OnDrawGizmos()
    {
        if (_path != null)
        {
            Gizmos.color = Color.yellow; // ��θ� ��������� ǥ��

            for (int i = 0; i < _path.Count - 1; i++)
            {
                Gizmos.DrawLine(_path[i].Position, _path[i + 1].Position); // ��� ���̿� �� �׸���
            }
        }
    }
}