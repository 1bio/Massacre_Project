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
    Dead,
    Movement,
    GotHit,
    Null
}

public class Monster : MonoBehaviour
{
    public MonsterStateType MonsterStateType { get; set; }
    public MonsterStateMachineController MonsterStateMachineController { get; private set; }

    // 몬스터 능력치
    [Header(" # 몬스터 데이터")]
    [SerializeField] private MonsterStatData _monsterData;

    public MonsterMovementController MovementController { get; private set; }
    public MonsterAnimationController AnimationController { get; private set; }
    public MonsterCombatController MonsterCombatController { get; private set; }

    private void Awake()
    {
        MonsterStateMachineController = GetComponent<MonsterStateMachineController>();

        MovementController = new MonsterMovementController(GetComponent<Astar>(), FindObjectOfType<PointGrid>(), GetComponent<CharacterController>());
        AnimationController = new MonsterAnimationController(GetComponent<Animator>(), GetComponent<ObjectFadeInOut>(),100f);
        MonsterCombatController = new MonsterCombatController(_monsterData, GetComponent<Health>());

        MonsterCombatController.MonsterCombatAbility.MonsterHealth.InitializeHealth();
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

    public void UnLockedInAnimation()
    {
        AnimationController.IsLockedInAnimation = false;
    }
}