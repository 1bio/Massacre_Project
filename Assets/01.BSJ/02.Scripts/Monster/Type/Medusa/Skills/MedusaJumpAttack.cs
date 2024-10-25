using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MedusaJumpAttack", menuName = "Data/MonsterSKillData/Medusa/JumpAttack")]
public class MedusaJumpAttack : MonsterSkillData
{
    private Transform _vfxTransform;

    private Transform _swordObjectTransform;
    private string _swordHierarchyPath = "Root/Hips/Spine/Spine1/Spine2/LeftShoulder/LeftArm/LeftForeArm/LeftWrist/LeftHand/L_weapon/Medusa_Sword/SwordPosition";

    private float _hitSphere;

    private int _currentAttackCount;
    private int _maxAttackLimit;

    private HashSet<Collider> damagedPlayers = new HashSet<Collider>();
    private bool _hasAttacked = false;
    private bool _hasGroundHit = false;

    private void InitializeValues(Monster monster)
    {
        _vfxTransform = monster.MonsterParticleController.VFX["SmokeCircle"].transform;

        _swordObjectTransform = monster.transform.Find(_swordHierarchyPath);

        _hitSphere = 3f;

        _currentAttackCount = 0;
        _maxAttackLimit = 3;

        damagedPlayers = new HashSet<Collider>();
        _hasGroundHit = false;
    }

    public override void ActiveSkillEnter(Monster monster)
    {
        InitializeValues(monster);
    }

    public override void ActiveSkillTick(Monster monster)
    {
        monster.AnimationController.AnimatorStateInfo = monster.AnimationController.Animator.GetCurrentAnimatorStateInfo(0);

        if (monster.AnimationController.AnimatorStateInfo.IsTag("JumpAttack"))
        {
            if (monster.AnimationController.AnimatorStateInfo.normalizedTime < 0.9)
            {
                ActivateVFXIfHitGround(monster);
            }
            else
            {
                if (_currentAttackCount >= _maxAttackLimit)
                    monster.AnimationController.IsLockedInAnimation = false;
            }
        }
        else
        {
            damagedPlayers = new HashSet<Collider>();
            _hasGroundHit = false;
            _hasAttacked = false;

            monster.AnimationController.PlayIdleAnimation();
            monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);
            
            if (Vector3.Angle(monster.transform.forward, monster.MovementController.Direction) <= 3 && !_hasAttacked)
                monster.AnimationController.PlaySkillAnimation(Medusa.JumpAttackAnimationName.JumpAttack.ToString());
        }
    }

    public override void ActiveSkillExit(Monster monster)
    {

    }

    private void ActivateVFXIfHitGround(Monster monster)
    {
        if (!_hasGroundHit)
        {
            Vector3 rayOrigin = _swordObjectTransform.position;
            Vector3 rayDirection = Vector3.down;

            float rayLength = 1f;

            int layerMask = (1 << LayerMask.NameToLayer(GameLayers.Ground.ToString()));
            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayLength, layerMask))
            {
                ActivateVFX(monster.transform.position + monster.transform.forward * _hitSphere, monster);
            }

            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red, 2f);

            _currentAttackCount += _hasAttacked ? 0 : 1;
            _hasAttacked = true;
        }
    }

    private void ActivateVFX(Vector3 position, Monster monster)
    {
        if (_vfxTransform != null)
        {
            _vfxTransform.position = position;
            monster.MonsterParticleController.RePlayVFX(_vfxTransform.name, _hitSphere / 3);
            Hit(position, monster);
        }
    }

    private void Hit(Vector3 position, Monster monster)
    {
        int layermask = (1 << LayerMask.NameToLayer(GameLayers.Player.ToString()));
        Collider[] colliders = Physics.OverlapSphere(position, _hitSphere, layermask);

        foreach (Collider collider in colliders)
        {
            if (damagedPlayers.Add(collider))
            {
                collider.gameObject.GetComponent<Health>()?.TakeDamage(monster.MonsterSkillController.CurrentSkillData.Damage, true);
            }
        }
    }

}
