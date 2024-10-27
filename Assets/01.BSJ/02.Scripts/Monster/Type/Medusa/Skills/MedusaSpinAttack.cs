using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MedusaSpinAttack", menuName = "Data/MonsterSKillData/Medusa/SpinAttack")]
public class MedusaSpinAttack : MonsterSkillData
{
    private Transform _vfxTransform;

    private bool _hasAttacked = false;

    private void InitializeValues(Monster monster)
    {
        _vfxTransform = monster.MonsterParticleController.VFX["SpinEffect"].transform;
        _vfxTransform.SetParent(monster.transform);
        _vfxTransform.localPosition = Vector3.up;
        _vfxTransform.localRotation = Quaternion.identity;

        _hasAttacked = false;
    }

    public override void ActiveSkillEnter(Monster monster)
    {
        InitializeValues(monster);
    }

    public override void ActiveSkillTick(Monster monster)
    {
        monster.AnimationController.AnimatorStateInfo = monster.AnimationController.Animator.GetCurrentAnimatorStateInfo(0);

        if (monster.AnimationController.AnimatorStateInfo.IsName(Medusa.SpinAttackAnimationName.SpinAttack.ToString()))
        {

        }
        else
        {
            monster.MovementController.LookAtTarget(monster.MonsterCombatController.MonsterCombatAbility.TurnSpeed * 2);
            if (Vector3.Angle(monster.transform.forward, monster.MovementController.Direction) <= 3 && !_hasAttacked)
            {
                monster.AnimationController.PlaySkillAnimation(Medusa.SpinAttackAnimationName.SpinAttack.ToString());
            }
        }
    }

    public override void ActiveSkillExit(Monster monster)
    {
    
    }
}
