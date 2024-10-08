using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementController
{
    private Astar _astar;
    private PointGrid _pointGrid;
    private Vector3 _direction;
    private CharacterController _characterController;

    public MonsterMovementController(Astar astar, PointGrid pointGrid, CharacterController characterController)
    {
        _astar = astar;
        _pointGrid = pointGrid;
        _characterController = characterController;
    }

    public Astar Astar => _astar;
    public PointGrid PointGrid => _pointGrid;
    public List<PointNode> Path => _astar.Path;
    public Vector3 Direction => _direction;
    public CharacterController CharacterController => _characterController;

    public void LookAtTarget(Transform target)
    {
        Vector3 targetPos = target.position;
        targetPos.y = _characterController.transform.position.y;

        _direction = (targetPos - _characterController.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(_direction);

        _characterController.transform.rotation = Quaternion.Slerp(_characterController.transform.rotation, lookRotation, Time.deltaTime);
    }
}
