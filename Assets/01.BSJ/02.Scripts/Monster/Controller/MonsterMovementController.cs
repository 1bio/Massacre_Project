using DG.Tweening;
using System.Collections.Generic;
using TMPro;
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

    public void StepToNode(PointNode nextNode, Monster monster, int pathIndex)
    {
        Vector3 startNode = monster.transform.position;
        Vector3 targetNode = Path[pathIndex].Position;
        Vector3 direction = (targetNode - startNode).normalized;
        float speed = monster.MonsterCombatController.MonsterCombatAbility.MoveSpeed * monster.AnimationController.LocomotionBlendValue;

        if (Vector3.Distance(startNode, targetNode) > 0.1f)
        {
            LookAtNode(startNode + direction, monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);
        }

        Vector3 newPosition = direction * speed;
        CharacterController.SimpleMove(newPosition);
    }

    public void LookAtTarget(float rotationSpeed)
    {
        Vector3 targetPos = Astar.TargetTransform.position;
        targetPos.y = CharacterController.transform.position.y;

        Direction = (targetPos - CharacterController.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Direction);

        CharacterController.transform.rotation = Quaternion.RotateTowards(CharacterController.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    public void LookAtNode(Vector3 nodePosition, float rotationSpeed)
    {
        nodePosition.y = CharacterController.transform.position.y;

        Direction = (nodePosition - CharacterController.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(Direction);

        float angle = Vector3.Angle(Direction, CharacterController.transform.forward);
        Debug.Log(angle);

        float currentRotationSpeed = (angle > 45) ? rotationSpeed : rotationSpeed * 0.25f;

        CharacterController.transform.rotation = Quaternion.RotateTowards(CharacterController.transform.rotation, lookRotation, currentRotationSpeed * Time.deltaTime);

        if (angle < 1f) // 각도가 1도 이내로 가깝다면
        {
            CharacterController.transform.rotation = lookRotation; // 직접적으로 목표 회전으로 설정
        }
    }
}
