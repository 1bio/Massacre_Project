using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointGrid : MonoBehaviour
{
    public float NodeRadius { get => nodeRadius; }


    [SerializeField] private Vector3 gridWorldSize; // (x, y, z)
    [SerializeField] private float nodeRadius;
    private PointNode[,,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY, gridSizeZ;

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = 1;
        gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new PointNode[gridSizeX, gridSizeY, gridSizeZ];

        Vector3 worldBottomLeft = this.transform.position - Vector3.right * gridWorldSize.x / 2
                                                          - Vector3.forward * gridWorldSize.z / 2;


        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                                                    + Vector3.forward * (z * nodeDiameter + nodeRadius);

                    bool isObstacle = false;
                    bool isGround = false;

                    Collider[] hitColliders = Physics.OverlapSphere(worldPoint, nodeRadius);

                    foreach (var hitCollider in hitColliders)
                    {
                        if (hitCollider.CompareTag("Obstacle"))
                        {
                            isObstacle = true;
                            break;
                        }

                        if (hitCollider.CompareTag("Ground"))
                        {
                            isGround = true;
                            break;
                        }
                    }

                    grid[x, y, z] = new PointNode(worldPoint, isObstacle, isGround);
                }
            }
        }
    }

    public List<PointNode> GetNeighborNodes(PointNode node)
    {
        List<PointNode> neighbors = new List<PointNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0)
                    continue;

                int checkX = Mathf.FloorToInt(node.Position.x + x);
                int checkZ = Mathf.FloorToInt(node.Position.z + z);

                if (checkX >= 0 && checkX < gridSizeX && checkZ >= 0 && checkZ < gridSizeZ
                    && grid[checkX, 0, checkZ].IsGround && !grid[checkX, 0, checkZ].IsObstacle)
                {
                    neighbors.Add(grid[checkX, 0, checkZ]);
                }   
            }
        }

        return neighbors;
    }

    public void InitializeNodeValues()
    {
        foreach (PointNode node in grid)
        {
            node.Initialize();
        }
    }

    public PointNode GetPointNodeFromGridByPosition(Vector3 position)
    {
        foreach (PointNode node in grid)
        {
            if (node.Position.x - nodeRadius <= position.x && node.Position.x + nodeRadius > position.x
                && node.Position.z - nodeRadius <= position.z && node.Position.z + nodeRadius > position.z)
                return node;
        }

        return null; 
    }

    // 만든 grid 큐브로 시각화
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, gridWorldSize.z));

        if (grid != null)
        {
            foreach (PointNode node in grid)
            {
                if (node.IsObstacle || !node.IsGround)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
