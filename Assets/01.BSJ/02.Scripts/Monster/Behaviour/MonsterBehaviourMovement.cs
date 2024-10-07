using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MonsterBehaviourMovement : MonsterBehaviour
{
    private Rigidbody _rigidbody;
    private PointGrid _pointGrid;
    private List<PointNode> _neighborNodes;
    private List<PointNode> _path;
    private int _pathIndex;
    private bool _isMoving;

    public override void OnBehaviourStart(Monster monster)
    {
        monster.SetWalkAnimation();

        _rigidbody = monster.Rigidbody;
        _pointGrid = monster.PointGrid;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (monster == null || monster.Path == null)
        {
            monster.SetIdleAnimation();
            return;
        }

        if (_path != monster.Path && monster.Path.Count >= 1)
        {
            monster.SetWalkAnimation();
            _path = monster.Path;
            _pathIndex = 1;
        }

        if (_pathIndex < _path.Count && !_isMoving)
        {
            monster.LookAtTarget(monster);

            _neighborNodes = _pointGrid.GetNeighborNodes(_pointGrid.GetPointNodeFromGridByPosition(monster.transform.position));
            foreach (PointNode node in _neighborNodes)
            {
                node.IsObstacle = true;
            }

            StepToNode(_path[_pathIndex], monster, _pathIndex);

            if (_pointGrid.GetPointNodeFromGridByPosition(monster.transform.position) == _path[_pathIndex])
            {
                _pathIndex++;
            }
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        monster.SetIdleAnimation();
    }

    private void StepToNode(PointNode nextNode, Monster monster, int pathIndex)
    {
        _isMoving = true;

        Vector3 startNode = monster.transform.position;
        Vector3 targetNode = _path[pathIndex].Position;
        Vector3 direction = (targetNode - startNode).normalized;
        float speed = monster.MonsterAbility.MoveSpeed * monster.LocomotionBlendValue;
        Vector3 newPosition = startNode + direction * speed * Time.deltaTime;

        _rigidbody.MovePosition(newPosition);

        foreach (PointNode node in _neighborNodes)
        {
            node.IsObstacle = false;
        }
        _neighborNodes.Clear();

        _isMoving = false;
    }
}