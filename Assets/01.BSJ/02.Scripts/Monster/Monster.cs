//using MasterRealisticFX;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum MonsterStateType
{
    Spawn,
    Idle,
    Attack,
    Death,
    Walk,
    GotHit,
    Skill,
    Null
}

public class Monster : MonoBehaviour
{
    [Header(" # Stat Data")]
    [SerializeField] protected MonsterStatData p_monsterStatData;

    [Header(" # Skill Data")]
    [SerializeField] protected MonsterSkillData p_monsterSkillData;

    private Transform _vfxContainerTransform;
    private TrailRenderer _objectTrail;

    public MonsterStateType MonsterStateType { get; set; }
    public MonsterStateMachineController MonsterStateMachineController { get; private set; }
    public MonsterMovementController MovementController { get; protected set; }
    public MonsterAnimationController AnimationController { get; protected set; }
    public MonsterCombatController MonsterCombatController { get; protected set; }
    public MonsterParticleController MonsterParticleController { get; protected set; }
    public TrailRenderer ObjectTrail { get => _objectTrail; }

    protected virtual void Awake()
    {
        _vfxContainerTransform = transform.Find("VFXContainer");
        if (_vfxContainerTransform == null)
        {
            GameObject vfxContainer = new GameObject("VFXContainer");
            _vfxContainerTransform = vfxContainer.transform;

            _vfxContainerTransform.SetParent(transform);

            _vfxContainerTransform.localPosition = Vector3.zero;
        }

        _objectTrail = GetComponentInChildren<TrailRenderer>();
        if (_objectTrail != null)
        {
            _objectTrail.gameObject.SetActive(false);
        }

        MonsterStateMachineController = GetComponent<MonsterStateMachineController>();

        MovementController = new MonsterMovementController(GetComponent<TargetDetector>(), GetComponent<Astar>(), FindObjectOfType<PointGrid>(), GetComponent<CharacterController>());
        AnimationController = new MonsterAnimationController(GetComponent<Animator>(), GetComponent<ObjectFadeInOut>(),100f);

        if (p_monsterSkillData != null)
        {
            MonsterCombatController = new MonsterCombatController(p_monsterStatData, p_monsterSkillData, GetComponent<Health>());
            MonsterParticleController = new MonsterParticleController(MonsterCombatController.MonsterCombatAbility.MonsterSkillData, _vfxContainerTransform);
        }
        else
        {
            MonsterCombatController = new MonsterCombatController(p_monsterStatData, GetComponent<Health>());
        }
    }

    // Animation Event
    public void EnableWeapon()
    {
        MonsterCombatController.MonsterCombatAbility.MonsterAttack.IsEnableWeapon = true;
    }

    public void DisableWeapon()
    {
        MonsterCombatController.MonsterCombatAbility.MonsterAttack.IsEnableWeapon = false;
    }

    public void UnlockAnimationTransition()
    {
        AnimationController.IsLockedInAnimation = false;
    }
}