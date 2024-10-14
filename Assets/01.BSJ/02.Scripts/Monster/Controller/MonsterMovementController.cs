using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementController
{
    public MonsterMovementController(TargetDetector targetDetector, Astar astar, PointGrid pointGrid, CharacterController characterController)
    {
        TargetDetector = targetDetector;
        Astar = astar;
        PointGrid = pointGrid;
        CharacterController = characterController;
    }

    public TargetDetector TargetDetector { get; private set; }
    public Astar Astar { get; private set; }
    public PointGrid PointGrid { get; private set; }
    public List<PointNode> Path { get => Astar.Path; }
    public Vector3 Direction { get; private set; }
    public CharacterController CharacterController { get; private set; }

    public void LookAtTarget(float rotationSpeed)
    {
        Vector3 targetPos = Astar.TargetTransform.position;
        targetPos.y = CharacterController.transform.position.y;

        Direction = (targetPos - CharacterController.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Direction);

        CharacterController.transform.rotation = Quaternion.RotateTowards(CharacterController.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
