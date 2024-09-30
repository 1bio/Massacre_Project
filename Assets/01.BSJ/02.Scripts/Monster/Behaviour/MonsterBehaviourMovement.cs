using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MonsterBehaviourMovement : MonsterBehaviour
{
    private PointGrid pointGrid;
    private List<PointNode> path;
    private PointNode moveNode = null;
    private int pathIndex = 0;

    public override void OnBehaviourStart(Monster monster)
    {
        pointGrid = monster.PointGrid;
        path = monster.Path;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (path != monster.Path)
        {
            path = monster.Path;
            pathIndex = 0;
        }

        if (path == null)
            return;

        if (pathIndex < path.Count)
        {
            moveNode = path[pathIndex];
            StepToNode(moveNode, monster, pathIndex);

            if (pointGrid.GetPointNodeFromGridByPosition(monster.transform.position) == moveNode)
            {
                pathIndex++;
            }
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        
    }

    private void StepToNode(PointNode nextNode, Monster monster, int pathIndex)
    {
        Vector3 startNode = monster.transform.position;
        Vector3 targetNode;

        targetNode = path[pathIndex].Position;

        Vector3 direction = (targetNode - startNode).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, targetRotation, monster.MonsterAbility.TurnSpeed * Time.deltaTime);
        }

        monster.transform.position = Vector3.MoveTowards(startNode, targetNode, monster.MonsterAbility.MoveSpeed * Time.deltaTime);

        startNode = monster.transform.position;
    }
}