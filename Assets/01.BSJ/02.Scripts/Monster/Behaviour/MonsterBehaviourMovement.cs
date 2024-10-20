using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MonsterBehaviourMovement : MonsterBehaviour
{
    private CharacterController _characterController;
    private Monster _monster;
    private PointGrid _pointGrid;
    private List<PointNode> _neighborNodes;
    private List<PointNode> _path;
    private int _pathIndex;
    private bool _isMoving;

    public override void OnBehaviourStart(Monster monster)
    {
        _monster = monster;
        _pointGrid = monster.MovementController.PointGrid;
        _characterController = monster.MovementController.CharacterController;

        monster.AnimationController.PlayWalkAnimation();

        monster.MonsterCombatController.Health.ImpactEvent += OnImpact;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        monster.AnimationController.AnimatorStateInfo = monster.AnimationController.Animator.GetCurrentAnimatorStateInfo(0);

        if (monster == null || monster.MovementController.Path == null)
        {
            monster.MonsterStateMachineController.OnIdle();
            monster.MovementController.Astar.StartPathCalculation();
            return;
        }

        if (_path != monster.MovementController.Path && monster.MovementController.Path.Count >= 1)
        {
            monster.AnimationController.PlayWalkAnimation();
            _path = monster.MovementController.Path;
            _pathIndex = 1;
        }

        if (_pathIndex < _path.Count && !_isMoving)
        {
            monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);

            StepToNode(_path[_pathIndex], monster, _pathIndex);

            if (_pointGrid.GetPointNodeFromGridByPosition(monster.transform.position) == _path[_pathIndex])
            {
                _pathIndex++;
            }
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        monster.AnimationController.PlayIdleAnimation();
        monster.MonsterCombatController.Health.ImpactEvent -= OnImpact;
    }

    private void OnImpact()
    {
        _monster.MonsterStateMachineController.OnGotHit();
    }

    private void StepToNode(PointNode nextNode, Monster monster, int pathIndex)
    {
        _isMoving = true;

        Vector3 startNode = monster.transform.position;
        Vector3 targetNode = _path[pathIndex].Position;
        Vector3 direction = (targetNode - startNode).normalized;
        float speed = monster.MonsterCombatController.MonsterCombatAbility.MoveSpeed * monster.AnimationController.LocomotionBlendValue;
        Vector3 newPosition = direction * speed;

        _characterController.SimpleMove(newPosition);

        _isMoving = false;
    }
}