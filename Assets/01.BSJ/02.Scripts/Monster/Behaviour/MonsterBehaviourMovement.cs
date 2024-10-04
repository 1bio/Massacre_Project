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
    private PointNode _currentNode;
    private List<PointNode> _path;
    private int _pathIndex;
    private bool _isMoving;

    public override void OnBehaviourStart(Monster monster)
    {
        monster.SetWalkAnimation();

        if (_rigidbody == null)
            _rigidbody = monster.Rigidbody;
        if (_pointGrid == null)
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
            LookAtTarget(monster);
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

        _currentNode = _pointGrid.GetPointNodeFromGridByPosition(startNode);
        //_currentNode.IsObstacle = true;   // 길을 못 찾음

        _rigidbody.MovePosition(Vector3.MoveTowards(startNode, targetNode, /*monster.MonsterAbility.MoveSpeed **/ Time.deltaTime));
        
        //_currentNode.IsObstacle = false;

        _isMoving = false;
    }

    private void LookAtTarget(Monster monster)
    {
        Vector3 targetPos = monster.Astar.TargetTransform.position;
        Vector3 direction = (targetPos - monster.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.MonsterAbility.TurnSpeed * Time.deltaTime);
    }
}