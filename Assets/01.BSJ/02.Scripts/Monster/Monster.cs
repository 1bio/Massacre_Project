//using MasterRealisticFX;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;
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
    [SerializeField] protected MonsterSkillData[] p_monsterSkillDatas;

    [Header(" # Loot Item Data")]
    [SerializeField] protected MonsterLootItemData p_monsterLootItemData;

    private Transform _vfxContainerTransform;
    private TrailRenderer _objectTrail;

    public string VFXContainerName { get; } = "VFX Container";
    public MonsterStateType MonsterStateType { get; set; }
    public MonsterStateMachineController MonsterStateMachineController { get; private set; }
    public MonsterSkillController MonsterSkillController { get; private set; }
    public MonsterLootItemController MonsterLootItemController { get; private set; }
    public MonsterMovementController MovementController { get; protected set; }
    public MonsterAnimationController AnimationController { get; protected set; }
    public MonsterCombatController MonsterCombatController { get; protected set; }
    public MonsterParticleController MonsterParticleController { get; protected set; }
    public TrailRenderer ObjectTrail { get => _objectTrail; }

    // 넉백 처리
    public CharacterController Controller { get; protected set; }
    public ForceReceiver ForceReceiver { get; protected set; }

    protected virtual void Awake()
    {
        _vfxContainerTransform = transform.Find(VFXContainerName);

        _objectTrail = GetComponentInChildren<TrailRenderer>();
        if (_objectTrail != null)
        {
            _objectTrail.gameObject.SetActive(false);
        }

        MonsterStateMachineController = GetComponent<MonsterStateMachineController>();
        MonsterSkillController = new MonsterSkillController(p_monsterSkillDatas);
        MonsterLootItemController = new MonsterLootItemController(p_monsterLootItemData);
        MovementController = new MonsterMovementController(GetComponent<TargetDetector>(), GetComponent<Astar>(), FindObjectOfType<PointGrid>(), GetComponent<CharacterController>());
        AnimationController = new MonsterAnimationController(GetComponent<Animator>(), GetComponent<ObjectFadeInOut>(),33.3f);
        MonsterCombatController = new MonsterCombatController(p_monsterStatData, GetComponent<CreatureHealth>());
     
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        if (p_monsterSkillDatas.Length > 0)
        {
            MonsterParticleController = new MonsterParticleController(p_monsterSkillDatas, _vfxContainerTransform);
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

    public void RePlayVFX(string vfxNameWithScale)
    {
        if (vfxNameWithScale.Contains('_'))
        {
            string[] parts = vfxNameWithScale.Split('_');
            string vfxName = parts[0];
            float scaleFactor = float.Parse(parts[1]);

            MonsterParticleController.RePlayVFX(vfxName, scaleFactor);
        }
        else
        {
            MonsterParticleController.RePlayVFX(vfxNameWithScale);
        }
    }
}