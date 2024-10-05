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
    public MonsterStateType MonsterStateType
    {
        get => _monsterStateType;
        set => _monsterStateType = value;
    }

    // 몬스터 능력치
    public MonsterCombatAbility MonsterAbility
    {
        get => _monsterAbility;
        set => _monsterAbility = value;
    }
    public MonsterStatData MonsterStatData => _monsterData;

    // 길찾기 및 이동
    public Rigidbody Rigidbody => _rigidbody;
    public List<PointNode> Path => _path;
    public Astar Astar => _astar;
    public PointGrid PointGrid => _pointGrid;
    public Vector3 Direction => _direction;

    // 애니메이션
    public Animator Animator => _animator;
    public string CurrentAniamtionName => _currentAnimationName;
    public bool IsLockedInAnimation
    {
        get => _isLockedInAnimation;
        set => _isLockedInAnimation = value;
    }
    public float LocomotionBlendValue => _locomotionBlendValue;

    // 쿨타임
    public float BasicAttackCoolTimeCheck
    {
        get => _basicAttackCoolTimeCheck;
        set => _basicAttackCoolTimeCheck = value;
    }
    public float SkillAttackCoolTimeCheck 
    {
        get => _skillAttackCoolTimeCheck;
        set => _skillAttackCoolTimeCheck = value;
    }


    // 몬스터 상태
    [SerializeField] private MonsterStateType _monsterStateType = MonsterStateType.Spawn;

    // 몬스터 능력치
    private MonsterCombatAbility _monsterAbility;
    [SerializeField] private MonsterStatData _monsterData;

    // 길찾기 및 이동
    private Rigidbody _rigidbody;
    private Astar _astar;
    private PointGrid _pointGrid;
    private List<PointNode> _path;
    private Vector3 _direction;

    // 애니메이션
    private Animator _animator;
    private AnimatorClipInfo[] _nextClipInfo = null;   // 애니메이션 clip
    private bool _isLockedInAnimation = false;
    private string _currentAnimationName = string.Empty;    // 현재 애니메이션 이름
    private float _animationElapsedTime; // 애니메이션 경과 시간
    private float _locomotionBlendValue = 0f; // 0 = Idle, 1 = Walk
    [SerializeField] private float _blendTransitionSpeed = 100f;

    // 쿨타임
    [SerializeField] private float _basicAttackCoolTimeCheck;
    private float _skillAttackCoolTimeCheck;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _astar = GetComponent<Astar>();
        _pointGrid = FindObjectOfType<PointGrid>();

        _monsterAbility = new MonsterCombatAbility(_monsterData);
        _monsterAbility.MonsterHealth.InitializeHealth();
    }

    private void Start()
    {
        if (_astar.Path != null)
        {
            _path = _astar.Path;
        }
    }

    private void Update()
    {
        if (_path != _astar.Path)
        {
            _path = _astar.Path;
        }
    }

    // look at target
    public void LookAtTarget(Monster monster)
    {
        StartCoroutine(SmoothLookAtCoroutine(monster));
    }

    private IEnumerator SmoothLookAtCoroutine(Monster monster)
    {
        Vector3 targetPos = monster.Astar.TargetTransform.position;
        _direction = (targetPos - monster.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(_direction);

        monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.MonsterAbility.TurnSpeed * Time.deltaTime);

        yield return null;
    }


    // Move
    public void SetWalkAnimation()
    {
        SmoothLocomotionTransition(1);
    }

    public void SetIdleAnimation()
    {
        SmoothLocomotionTransition(0);
    }

    private void SmoothLocomotionTransition(int targetBlendValue)
    {
        _locomotionBlendValue = Mathf.Lerp(_locomotionBlendValue, targetBlendValue, _blendTransitionSpeed * Time.deltaTime);

        if (Mathf.Abs(_locomotionBlendValue - targetBlendValue) <= 0.1f)
        {
            _locomotionBlendValue = targetBlendValue;
        }

        _animator.SetFloat("Locomotion", _locomotionBlendValue);
    }


    // Attack
    public void SetRandomAttackAnimation()
    {
        SmoothLocomotionTransition(0);

        _animationElapsedTime = 0;
        MonsterAbility.MonsterAttack.IsAttack = true;
        _isLockedInAnimation = true;

        int randNum = Random.Range(0, MonsterAbility.MonsterAttack.AttackTotalCount) + 1;
        _currentAnimationName = $"Attack{randNum}";
        _animator.SetTrigger(_currentAnimationName);
    }


    // Got Hit
    public void SetGotHitAnimation()
    {
        SmoothLocomotionTransition(0);

        _animationElapsedTime = 0;
        _isLockedInAnimation = true;

        _currentAnimationName = "GotHit";
        _animator.SetTrigger(_currentAnimationName);
    }


    // Dead
    public void SetDeadAnimation()
    {
        SmoothLocomotionTransition(0);

        _animationElapsedTime = 0;
        _isLockedInAnimation = true;

        _currentAnimationName = "Death";
        _animator.SetTrigger(_currentAnimationName);
    }

    public void TakeDamage(int damge)
    {
        MonsterAbility.MonsterHealth.CurrentHealth -= damge;
        MonsterAbility.MonsterHealth.LastHealth = MonsterAbility.MonsterHealth.CurrentHealth;
        if (!_isLockedInAnimation)
        {
            MonsterAbility.MonsterHealth.IsHit = true;
        }
    }

    // Animation Event
    public void EnableWeapon()
    {
        _monsterAbility.MonsterAttack.IsEnableWeapon = true;
    }

    public void DisableWeapon()
    {
        _monsterAbility.MonsterAttack.IsEnableWeapon = false;
    }

    public void UnLockedInAnimation()
    {
        _isLockedInAnimation = false;
    }
}