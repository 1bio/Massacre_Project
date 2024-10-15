using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
        _hasAttacked = false;
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
        if (_vfxTransform == null) return;

        _firePositionTransform = monster.transform.Find("Ogre_root/Hips/Spine/Spine1/Spine2/Neck/Head/lip_top_m/" + _firePositionNameToFind);
        if (_firePositionTransform == null) return;

        _vfxTransform.position = _firePositionTransform.position;
        _vfxTransform.rotation = _firePositionTransform.rotation;
    }
}
