using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSkillData", menuName = "Data/MonsterSKillData/Minotaur/RamAttack")]
public class MinotaurRamAttack : MonsterSkillData
{
    private Minotaur _minotaur;

    private int _currentAttackCount = 0;
    private int _maxAttackLimit = 3;    // 최대 공격 횟수

    private bool _hasRamStarted = false;
    private bool _hasHitObject = false;

    // Raycast 관련
    private RaycastHit _hit;
    private float _maxDistance = 0.5f;
    private float _detectionRadius = 0.4f;

    //private Indicator _indicator;

    public override void ActiveSkillEnter(Monster monster)
    {
        _minotaur = (Minotaur) monster;
        monster.ObjectTrail.gameObject.SetActive(true);

        //_indicator = monster.GetComponentInChildren<Indicator>(true);
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
        monster.ObjectTrail.gameObject.SetActive(false);
    }

    private void StartRamAttack(Monster monster)
    {
        //_indicator.gameObject.SetActive(true);

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
            if (IsInFanShapeDetection(monster))
            {
                HandleRaycastHit(monster);
            }
            else
            {
                _hasHitObject = false;
            }
            monster.MovementController.CharacterController.SimpleMove(monster.transform.forward * monster.MonsterCombatController.MonsterCombatAbility.MoveSpeed * 2f);
        }
        else if (stateInfo.IsName(Minotaur.RamAttackAnimationName.RamWall.ToString())
            && stateInfo.normalizedTime >= 0.8f)
        {
            _hasRamStarted = false;
        }
    }

    private bool IsInFanShapeDetection(Monster monster)
    {
        Vector3 direction = monster.transform.forward;

        int layerMask = (1 << LayerMask.NameToLayer(GameLayers.Player.ToString())) |
                        (1 << LayerMask.NameToLayer(GameLayers.Obstacle.ToString()));

        if (Physics.SphereCast(monster.transform.position, _detectionRadius, direction, out _hit, _maxDistance, layerMask))
        {
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()) ||
                _hit.collider.gameObject.layer == LayerMask.NameToLayer(GameLayers.Obstacle.ToString()))
            {
                return true;
            }
        }
        return false;
    }

    private void HandleRaycastHit(Monster monster)
    {
        AnimatorStateInfo stateInfo = monster.AnimationController.Animator.GetCurrentAnimatorStateInfo(0);

        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()))
        {
            if (!_hasHitObject &&
                !stateInfo.IsName(Minotaur.RamAttackAnimationName.RamAttack.ToString()))
            {
                monster.MovementController.CharacterController.SimpleMove(Vector3.zero);
                monster.AnimationController.PlaySkillAnimation(Minotaur.RamAttackAnimationName.RamAttack.ToString());
                _hasHitObject = true;
            }
        }
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(GameLayers.Obstacle.ToString()))
        {
            if (!_hasHitObject &&
                !stateInfo.IsName(Minotaur.RamAttackAnimationName.RamWall.ToString()) &&
                !stateInfo.IsName(Minotaur.RamAttackAnimationName.RamAttack.ToString()))
            {
                _currentAttackCount++;
                monster.MovementController.CharacterController.SimpleMove(Vector3.zero);

                if (CheckIfAttackCountExceedsLimit())
                {
                    monster.AnimationController.PlaySkillAnimation(Minotaur.RamAttackAnimationName.RamAttack.ToString());
                }
                else
                {
                    monster.AnimationController.PlaySkillAnimation(Minotaur.RamAttackAnimationName.RamWall.ToString());
                }

                //_indicator.gameObject.SetActive(false);
                _hasHitObject = true;
            }
        }
    }

    private bool CheckIfAttackCountExceedsLimit()
    {
        return _currentAttackCount >= _maxAttackLimit;
    }
}