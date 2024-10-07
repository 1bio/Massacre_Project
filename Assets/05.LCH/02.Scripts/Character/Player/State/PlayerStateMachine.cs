using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("클래스")]
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public Targeting Targeting { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public MeleeComponenet MeleeComponenet { get; private set; }

    [field: SerializeField] public RangeComponent RangeComponent { get; private set; }


    [field: Header("컴포넌트")]
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Transform CameraTransform { get; private set; }

    [field: SerializeField] public GameObject[] WeaponPrefabs { get; private set; } // 임시


    [field: Header("플레이어 세팅")]
    [field: SerializeField] public float RotationSpeed { get; private set; }

    [field: SerializeField] public float MeleeWeaponDetectionRange { get; private set; } = 5f;

    [field: SerializeField] public float RangeWeaponDetectionRange { get; private set; } = 15f;


    [field: Header("능력치")]
    [field: SerializeField] public float CurrentHealth { get; private set; }

    [field: SerializeField] public float MoveSpeed { get; private set; }


    // 초기화
    private void Start()
    {
        MoveSpeed = DataManager.instance.playerData.statusData.moveSpeed; // 이동 속도 
        CurrentHealth = DataManager.instance.playerData.statusData.maxHealth; // 체력

        ChangeState(new PlayerFreeLookState(this));

        DataManager.instance.SaveData();
    }
}
