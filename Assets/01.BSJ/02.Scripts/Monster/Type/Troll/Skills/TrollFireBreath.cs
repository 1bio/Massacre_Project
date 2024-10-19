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

    private Transform _firePositionTransform;
    private string _firePosHierarchyPath = "Ogre_root/Hips/Spine/Spine1/Spine2/Neck/Head/lip_top_m/FirePos";

    private bool _hasAttacked = false;

    private void InitializeValues()
    {
        _hasAttacked = false;
    }

    public override void ActiveSkillEnter(Monster monster)
    {
        FindAndManipulateChild(monster);
        InitializeValues();
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

        _firePositionTransform = monster.transform.Find(_firePosHierarchyPath);
        if (_firePositionTransform == null) return;

        _vfxTransform.SetParent(_firePositionTransform);
        _vfxTransform.localPosition = Vector3.zero;
        _vfxTransform.localRotation = Quaternion.identity;
    }
}
