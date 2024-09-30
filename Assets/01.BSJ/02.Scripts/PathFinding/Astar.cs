using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Astar : MonoBehaviour
{
    public List<PointNode> Path => path;
    public bool HasTargetMoved => hasTargetMoved;

    private Monster monster;
    private PointGrid grid;

    private Transform startTransform;
    private Transform targetTransform;
    private Vector3 lastTargetPos;

    [SerializeField] private List<PointNode> path;
    private bool hasTargetMoved;
    private bool isCalculating = false;

    // FindPath ȣ���� �Ÿ�

    private void Awake()
    {
        grid = FindObjectOfType<PointGrid>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        monster = GetComponent<Monster>();
        lastTargetPos = targetTransform.position;
    }

    private void Start()
    {
        StartCoroutine(CalculatePathCoroutine());
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            OnHasTargetMoved();
        }

        if (hasTargetMoved && !isCalculating)
        {
            StartCoroutine(CalculatePathCoroutine());
        }
    }

    private IEnumerator CalculatePathCoroutine()
    {
        isCalculating = true;
        grid.InitializeNodeValues();
        path = FindPath(this.transform.position, targetTransform.position);
        lastTargetPos = targetTransform.position;
        isCalculating = false;

        yield return null; // ���� ���������� �̵�
    }

    public List<PointNode> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        PointNode startNode = grid.GetPointNodeFromGridByPosition(startPos);
        PointNode targetNode = grid.GetPointNodeFromGridByPosition(targetPos);

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
            List<PointNode> neighborNodes = grid.GetNeighborNodes(currentNode);

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
        if (lastTargetPos.x + monster.MonsterAbility.MaxTargetDistance < targetTransform.position.x
            || lastTargetPos.x - monster.MonsterAbility.MaxTargetDistance > targetTransform.position.x
            || lastTargetPos.z + monster.MonsterAbility.MaxTargetDistance < targetTransform.position.z
            || lastTargetPos.z - monster.MonsterAbility.MaxTargetDistance > targetTransform.position.z)
            hasTargetMoved = true;
        else
            hasTargetMoved = false;
    }

    // OnDrawGizmos �޼��� �߰��Ͽ� path �ð�ȭ
    private void OnDrawGizmos()
    {
        if (path != null)
        {
            Gizmos.color = Color.yellow; // ��θ� ��������� ǥ��

            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i].Position, path[i + 1].Position); // ��� ���̿� �� �׸���
            }
        }
    }
}