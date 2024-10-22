using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSkillData", menuName = "Data/MonsterSKillData/Troll/ChargeAttack")]
public class TrollChargeAttack : MonsterSkillData
{
    [Header(" # VFX  Name")]
    [SerializeField] private string _vfxNameToFind;
    private Transform _vfxTransform;

    private Transform _swordObjectTransform;
    private string _swordHierarchyPath = "Ogre_root/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightHand/Right_weapon/WeaponTrails/Sword";

    private float _hitSphere;

    private bool _hasAttacked = false;
    private bool _hasGroundHit = false;

    private void InitializeValues()
    {
        _hitSphere = 2f;
        _hasAttacked = false;
        _hasGroundHit = false;
    }

    public override void ActiveSkillEnter(Monster monster)
    {
        FindVFXAndSwordTransform(monster);
        InitializeValues();
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

    public void FindVFXAndSwordTransform(Monster monster)
    {
        _vfxTransform = monster.transform.Find("VFX Container/" + _vfxNameToFind);
        _swordObjectTransform = monster.transform.Find(_swordHierarchyPath);
    }

    private void ActivateVFXIfHitGround(Monster monster)
    {
        if (!_hasGroundHit)
        {
            Vector3 rayOrigin = _swordObjectTransform.position + new Vector3(0, 0, 0.66f);
            Vector3 rayDirection = Vector3.down;

            float rayLength = 0.5f;

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
            monster.MonsterParticleController.RePlayVFX(_vfxTransform.name);
        }
    }

    private void Hit(Vector3 position)
    {
        int layermask = (1 << LayerMask.NameToLayer(GameLayers.Player.ToString()));
        Collider[] colliders = Physics.OverlapSphere(position, _hitSphere, layermask);


    }
}
