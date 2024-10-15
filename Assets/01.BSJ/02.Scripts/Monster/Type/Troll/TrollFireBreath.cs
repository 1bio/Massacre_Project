using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSkillData", menuName = "Data/MonsterSKillData/Troll/FireBreath")]
public class TrollFireBreath : MonsterSkillData
{
    [Header(" # VFX  Name")]
    [SerializeField] private string _vfxNameToFind;
    private Transform _vfxTransform;

    [Header(" # Fire Postion")]
    [SerializeField] private string _firePositionNameToFind;
    private Transform _firePositionTransform;

    private bool _hasAttacked = false;

    public override void ActiveSkillEnter(Monster monster)
    {
        FindAndManipulateChild(monster);
    }

    public override void ActiveSkillTick(Monster monster)
    {
        if (!monster.AnimationController.AnimatorStateInfo.IsName(Troll.FireBreathAnimationName.FireBreath.ToString()))
        {
            monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed);
        }

        if (!_hasAttacked)
        {
            monster.AnimationController.PlaySkillAnimation(Troll.FireBreathAnimationName.FireBreath.ToString());
            _hasAttacked = true;
        }
    }

    public override void ActiveSkillExit(Monster monster)
    {
        

    }

    public void FindAndManipulateChild(Monster monster)
    {
        _vfxTransform = monster.transform.Find("VFX Container/" + _vfxNameToFind);
        if (_vfxTransform != null)
        {
            _firePositionTransform = monster.transform.Find("Ogre_root/Hips/Spine/Spine1/Spine2/Neck/Head/lip_top_m/" + _firePositionNameToFind);

            if (_firePositionTransform != null)
            {
                _vfxTransform.gameObject.transform.SetParent(_firePositionTransform);
                _vfxTransform.transform.localPosition = Vector3.zero;
                _vfxTransform.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
