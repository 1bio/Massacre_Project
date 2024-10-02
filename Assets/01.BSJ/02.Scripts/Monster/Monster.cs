using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public enum MonsterStateType
{
    Spawn,
    Attack,
    Dead,
    Movement,
    Null
}

public enum MonsterMovementControlType
{
    PlayerControlled,
    AnimationDriven
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

    // 애니메이션
    public Animator Animator => _animator;
    public string CurrentAniamtionName => _currentAnimationName;
    public bool IsLockedInAnimation
    {
        get => _isLockedInAnimation;
        set => _isLockedInAnimation = value;
    }

    // 몬스터 상태
    private MonsterMovementControlType _movementType = MonsterMovementControlType.PlayerControlled;
    [SerializeField] private MonsterStateType _monsterStateType = MonsterStateType.Spawn;

    // 몬스터 능력치
    private MonsterCombatAbility _monsterAbility;
    [SerializeField] private MonsterStatData _monsterData;

    // 길찾기 및 이동
    private Rigidbody _rigidbody;
    private Astar _astar;
    private PointGrid _pointGrid;
    private List<PointNode> _path;

    // 애니메이션
    private Animator _animator;
    private AnimatorClipInfo[] _nextClipInfo = null;   // 애니메이션 clip
    private bool _isLockedInAnimation = false;
    private string _currentAnimationName = string.Empty;    // 현재 애니메이션 이름
    private float _animationElapsedTime; // 애니메이션 경과 시간

    private float _locomotionSpeed = 0f;
    private float _previousLocomotionSpeed = 0f;
    [SerializeField] private float _locomotionSpeedChangeRate = 10f;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _astar = GetComponent<Astar>();
        _pointGrid = FindObjectOfType<PointGrid>();

        _monsterAbility = new MonsterCombatAbility(_monsterData);
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

    public float GetDistanceToTarget()
    {
        // target 여부 확인 및 거리 체크
        return 0;
    }

    public void SetMovementControl(MonsterMovementControlType moveType)
    {
        this._movementType = moveType;

        if (_movementType == MonsterMovementControlType.AnimationDriven)
        {
            _animator.applyRootMotion = true;
        }
        else
        {
            _animator.applyRootMotion = false;
        }
    }
    
    public void UnLockAnimation(string animationName)
    {
        _animationElapsedTime += Time.deltaTime;

        if (MonsterAbility.MonsterAttack.IsAttack)
        {
            if (_animator.IsInTransition(0))
            {
                _nextClipInfo = _animator.GetNextAnimatorClipInfo(0);
            }

            if (_nextClipInfo.Length > 0)
            {
                StartCoroutine(CheckAndUnlockAnimation(animationName));
            }
        }
    }

    private IEnumerator CheckAndUnlockAnimation(string animationName)
    {
        foreach (AnimatorClipInfo clipInfo in _nextClipInfo)
        {
            float clipLength = clipInfo.clip.length;

            Debug.Log(clipInfo.clip.name);

            if (clipInfo.clip.name == animationName)
            {
                if (clipLength * 0.8f <= _animationElapsedTime)
                {
                    _isLockedInAnimation = false;
                    MonsterAbility.MonsterAttack.IsAttack = false;
                    _nextClipInfo = null;
                    break;
                }
            }
        }

        yield return null;
    }

    // Move
    public void SetWalkAnimation()
    {
        _animator.SetFloat("Locomotion", 1);
        /*SetLocomotionAnimation(1);*/
    }

    public void SetIdleAnimation()
    {
        _animator.SetFloat("Locomotion", 0);
        /*SetLocomotionAnimation(0);*/
    }

    private void SetLocomotionAnimation(int targetSpeed)
    {
        if (_locomotionSpeed < targetSpeed)
        {
            _locomotionSpeed += Time.deltaTime * _locomotionSpeedChangeRate;
            if (_locomotionSpeed > targetSpeed)
                _locomotionSpeed = targetSpeed;
        }
        else if (_locomotionSpeed > targetSpeed)
        {
            _locomotionSpeed -= Time.deltaTime * _locomotionSpeedChangeRate;
            if (_locomotionSpeed < targetSpeed)
                _locomotionSpeed = targetSpeed;
        }

        if (_locomotionSpeed != _previousLocomotionSpeed)
        {
            _animator.SetFloat("Locomotion", _locomotionSpeed);
            _previousLocomotionSpeed = _locomotionSpeed;
        }

        _currentAnimationName = "Locomotion";
    }

    // Attack
    public void SetRandomAttackAnimation(int totalAttackCount)
    {
        _animationElapsedTime = 0;
        MonsterAbility.MonsterAttack.IsAttack = true;

        int randNum = Random.Range(0, totalAttackCount) + 1;
        _currentAnimationName = $"Attack {randNum}";
        _animator.SetTrigger(_currentAnimationName);
    }


    // Animation Event
    public void SetAttack()
    {
        _monsterAbility.MonsterAttack.IsAttack = true;
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            _monsterAbility.MonsterAttack.IsAttack = false;
        }
    }
}