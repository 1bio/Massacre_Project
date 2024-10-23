using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : Monster
{
    private bool _isJumping = false;

    public enum JumpAttackAnimationName
    {
        JumpAttack
    }

    private void Update()
    {
        if (MonsterCombatController.MonsterCombatAbility.MonsterHealth.CurrentHealth > 0)
        {
            JumpAttack();
        }
    }

    private void JumpAttack()
    {
        if (_isJumping &&
            Vector3.Distance(transform.position, MovementController.Astar.TargetTransform.position) > 1)
            MovementController.CharacterController.SimpleMove(MovementController.Direction * 10f);
    }

    // Animation Event
    public void StartJumpAttackMove()
    {
        _isJumping = true;
    }

    public void StopJumpAttackMove()
    {
        _isJumping = false;
    }
}