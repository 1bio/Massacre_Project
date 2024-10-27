using MasterRealisticFX;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("클래스")]
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public MeleeComponenet MeleeComponenet { get; private set; }

    [field: SerializeField] public WeaponToggle WeaponToggle { get; private set; }

    [field: SerializeField] public RangeComponent RangeComponent { get; private set; }

    [field: SerializeField] public Targeting Targeting { get; private set; }

    [field: SerializeField] public CameraShake CameraShake { get; private set; }

    [field: SerializeField] public VFXController VFXController { get; private set; }

    [field: SerializeField] public SimpleWeaponTrail WeaponTrail{ get; private set; }

    [field: SerializeField] public ParticleEventHandler ParticleEventHandler { get; private set; }


    [field: Header("컴포넌트")]
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Transform CameraTransform { get; private set; }

    [field: SerializeField] public GameObject[] WeaponPrefabs { get; private set; }


    [field: Header("플레이어 세팅")]
    [field: SerializeField] public float RotationSpeed { get; private set; }

    [field: SerializeField] public float DodgeDuration { get; private set; }

    [field: SerializeField] public float DodgeLenght { get; private set; }

    [field: SerializeField] public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    [field: SerializeField] public float DodgeCooldown { get; private set; }


    private void OnEnable()
    {
        Health.ImpactEvent += OnHandleTakeDamage;
    }

    private void OnDisable()
    {
        Health.ImpactEvent -= OnHandleTakeDamage;
    }

    public void SetDodgeTime(float dodgeTime)
    {
        PreviousDodgeTime = dodgeTime;
    }
  
    // 초기화
    private void Start()
    {
        DataManager.instance.SaveData();

        ChangeState(new PlayerFreeLookState(this));
    }


    #region Event Methods
    // Impact
    void OnHandleTakeDamage()
    {
        ChangeState(new PlayerImpactState(this));
    }

    // Groggy
    /* void OnHandleGroggy()
     {
         ChangeState(new PlayerGroggyState(this));
     }*/

    // Dead
    /*void OnHandleDie()
    {
        ChangeState(new PlayerDeadState(this));
    }*/
    #endregion

    protected void CoolDownEvent(string skillName)
    {
        SkillManager.instance.StartCooldown(skillName);
    }


}
