using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "TrollFireBreath", menuName = "Data/MonsterSKillData/Troll/FireBreath")]
public class TrollFireBreath : MonsterSkillData
{
    private Troll _troll;
    private Transform _vfxTransform;

    private bool _hasAttacked = false;

    private void InitializeValues(Monster monster)
    {
        _troll = (Troll) monster;

        _vfxTransform = monster.MonsterParticleController.VFX["RedFlameThrower"].transform;
        _vfxTransform.SetParent(_troll.FirePositionTransform);
        _vfxTransform.position = _troll.FirePositionTransform.position;
        _vfxTransform.rotation = _troll.FirePositionTransform.rotation;

        _hasAttacked = false;
    }

    public override void ActiveSkillEnter(Monster monster)
    {
        InitializeValues(monster);
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
}