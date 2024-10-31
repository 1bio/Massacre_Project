using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TrollChargeAttack", menuName = "Data/MonsterSKillData/Troll/ChargeAttack")]
public class TrollChargeAttack : MonsterSkillData
{
    private Transform _vfxTransform;

    private Transform _swordObjectTransform;
    private string _swordHierarchyPath = "Ogre_root/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand/Right_weapon/WeaponTrails/Sword";

    private float _hitSphere;

    private HashSet<Collider> damagedPlayers = new HashSet<Collider>();
    private bool _hasAttacked = false;
    private bool _hasGroundHit = false;

    private void InitializeValues(Monster monster)
    {
        _vfxTransform = monster.MonsterParticleController.VFX["SmokeCircle"][0].transform;

        _swordObjectTransform = monster.transform.Find(_swordHierarchyPath);

        _hitSphere = 2f;

        damagedPlayers = new HashSet<Collider>();
        _hasAttacked = false;
        _hasGroundHit = false;
    }

    public override void ActiveSkillEnter(Monster monster)
    {
        InitializeValues(monster);
    }

    public override void ActiveSkillTick(Monster monster)
    {
        if (!monster.AnimationController.AnimatorStateInfo.IsName(Troll.ChargeAttackAnimationName.ChargeAttack.ToString()))
        {
            monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);
        }
        else
        {
            ActivateVFXIfHitGround(monster);
        }

        if (!_hasAttacked)
        {
            monster.AnimationController.PlaySkillAnimation(Troll.ChargeAttackAnimationName.ChargeAttack.ToString());
            _hasAttacked = true;
        }
    }

    public override void ActiveSkillExit(Monster monster)
    {
        
    }

    private void ActivateVFXIfHitGround(Monster monster)
    {
        if (!_hasGroundHit)
        {
            Vector3 rayOrigin = _swordObjectTransform.position + new Vector3(0, 0, 0.66f);
            Vector3 rayDirection = Vector3.down;

            float rayLength = 0.7f;

            int layerMask = (1 << LayerMask.NameToLayer(GameLayers.Ground.ToString()));
            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayLength, layerMask))
            {
                ActivateVFX(hit.point, monster);
            }

            Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red, 2f);
            _hasAttacked = true;
        }
    }

    private void ActivateVFX(Vector3 position, Monster monster)
    {
        if (_vfxTransform != null)
        {
            _vfxTransform.position = position;
            monster.MonsterParticleController.RePlayVFX(_vfxTransform.name, 0.8f);
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
