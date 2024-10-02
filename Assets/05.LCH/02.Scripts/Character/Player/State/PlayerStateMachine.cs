using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("클래스")]
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public Attack[] Attack { get; private set; }

    [field: SerializeField] public Targeting Targeting { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public MeleeWeaponComponent WeaponComponent { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public RangeWeaponComponent Projectile { get; private set; }


    [field: Header("컴포넌트")]
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Transform CameraTransform { get; private set; }

    [field: SerializeField] public GameObject[] WeaponPrefabs { get; private set; } // 임시


    [field: Header("값")]
    [field: SerializeField] public float MoveSpeed { get; private set; }

    [field: SerializeField] public float RotationSpeed { get; private set; }

    [field: SerializeField] public float RollingCoolTime { get; private set; } = 3f;

    [field: SerializeField] public float MeleeWeaponDetectionRange { get; private set; } = 5f;

    [field: SerializeField] public float RangeWeaponDetectionRange { get; private set; } = 15f;

    private void Start()
    {
        ChangeState(new PlayerFreeLookState(this));
    }

}
