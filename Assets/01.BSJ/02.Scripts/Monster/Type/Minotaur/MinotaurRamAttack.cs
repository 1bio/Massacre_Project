using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSkillData", menuName = "Data/MonsterSKillData/Minotaur/RamAttack")]
public class MinotaurRamAttack : MonsterSkillData
{
    private Minotaur _minotaur;
    private float _attackAngleThreshold = 20f;  // 공격 인식 각도

    private int _currentAttackCount = 0;
    private int _maxAttackLimit = 3;    // 최대 공격 횟수

    private bool _hasRamStarted = false;
    private bool _hasHitWall = false;

    // Raycast 관련
    private RaycastHit _hit;
    private float _detectionDistance = 1f;

    public override void ActiveSkillEnter(Monster monster)
    {
        _minotaur = (Minotaur) monster;
    }

    public override void ActiveSkillTick(Monster monster)
    {
        monster.AnimationController.AnimatorStateInfo = monster.AnimationController.Animator.GetCurrentAnimatorStateInfo(0);

        if (!_hasRamStarted)
        {
            StartRamAttack(monster);
        }

        HandleAnimationState(monster);
    }

    public override void ActiveSkillExit(Monster monster)
    {
        
    }

    private void StartRamAttack(Monster monster)
    {
        monster.MovementController.CharacterController.SimpleMove(Vector3.zero);
        monster.AnimationController.PlaySkillAnimation(Minotaur.RamAttackAnimationName.RamStart.ToString());
        _hasRamStarted = true;
    }

    private void HandleAnimationState(Monster monster)
    {
        AnimatorStateInfo stateInfo = monster.AnimationController.AnimatorStateInfo;

        if (stateInfo.IsName(Minotaur.RamAttackAnimationName.RamStart.ToString()))
        {
            monster.MovementController.CharacterController.SimpleMove(Vector3.zero);
            monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);
        }
        else if (stateInfo.IsName(Minotaur.RamAttackAnimationName.RamRun.ToString()))
        {
            if (Physics.Raycast(monster.transform.position, monster.transform.forward, out _hit, _detectionDistance))
            {
                HandleRaycastHit(monster);
            }
            else
            {
                _hasHitWall = false;
            }
            monster.MovementController.CharacterController.SimpleMove(monster.transform.forward * monster.MonsterCombatController.MonsterCombatAbility.MoveSpeed * 2f);
        }
        else if (stateInfo.IsName(Minotaur.RamAttackAnimationName.RamWall.ToString())
            && stateInfo.normalizedTime >= 0.8f)
        {
            _hasRamStarted = false;
        }
    }

    private void HandleRaycastHit(Monster monster)
    {
        if (_hit.collider.CompareTag("Player"))
        {
            monster.MovementController.CharacterController.SimpleMove(Vector3.zero);
            monster.AnimationController.PlaySkillAnimation(Minotaur.RamAttackAnimationName.RamAttack.ToString());
        }
        else if (_hit.collider.CompareTag("Obstacle"))
        {
            if (!_hasHitWall)
            {
                monster.MovementController.CharacterController.SimpleMove(Vector3.zero);

                if (CheckIfAttackCountExceedsLimit())
                {
                    monster.AnimationController.PlaySkillAnimation(Minotaur.RamAttackAnimationName.RamAttack.ToString());
                }
                else
                {
                    monster.AnimationController.PlaySkillAnimation(Minotaur.RamAttackAnimationName.RamWall.ToString());
                }

                _currentAttackCount++;
                _hasHitWall = true;
            }
        }
    }

    private bool CheckIfAttackCountExceedsLimit()
    {
        return _currentAttackCount >= _maxAttackLimit;
    }
}