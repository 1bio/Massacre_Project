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
    public MonsterStateType MonsterStateType { get; set; }
    public MonsterStateMachineController MonsterStateMachineController { get; private set; }

    // 몬스터 능력치
    [Header(" # Stat Data")]
    [SerializeField] protected MonsterStatData p_monsterStatData;

    public MonsterMovementController MovementController { get; protected set; }
    public MonsterAnimationController AnimationController { get; protected set; }
    public MonsterCombatController MonsterCombatController { get; protected set; }

    protected virtual void Awake()
    {
        MonsterStateMachineController = GetComponent<MonsterStateMachineController>();

        MovementController = new MonsterMovementController(GetComponent<Astar>(), FindObjectOfType<PointGrid>(), GetComponent<CharacterController>());
        AnimationController = new MonsterAnimationController(GetComponent<Animator>(), GetComponent<ObjectFadeInOut>(),100f);
        MonsterCombatController = new MonsterCombatController(p_monsterStatData, GetComponent<Health>());
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