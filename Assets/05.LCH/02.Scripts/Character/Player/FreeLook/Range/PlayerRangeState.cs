using UnityEngine;

public class PlayerRangeState : PlayerBaseState
{
    public readonly int FreeLookWithRange = Animator.StringToHash("FreeLookWithRange"); // 원거리 블렌드 트리

    public readonly int RangeVelocity = Animator.StringToHash("RangeVelocity"); // 애니메이션 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;


    public PlayerRangeState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithRange, CrossFadeDuration);

        stateMachine.InputReader.RollEvent += OnRolling; // 구르기 
        stateMachine.Health.ImpactEvent += OnImpact; // 피격 

        stateMachine.InputReader.AimingEvent += OnAiming; // 정조준
        stateMachine.InputReader.SkillEvent += OnSkill; // 화살비
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculatorMovement();

        Move(movement, deltaTime); // 이동

        Rotate(movement, deltaTime); // 회전

        if (Input.GetKeyDown(KeyCode.E)) { Swap(); }

        Debug.Log($"트리플샷 스킬 쿨타임: {stateMachine.CoolDownController.GetRemainingCooldown("트리플샷")}");

        // Attack
        if(stateMachine.InputReader.IsAttacking && stateMachine.CoolDownController.GetRemainingCooldown("트리플샷") <= 0)
        {
            stateMachine.ChangeState(new PlayerRangeRapidShotState(stateMachine));
            return;
        }
        
        if(stateMachine.InputReader.IsAttacking && stateMachine.CoolDownController.GetRemainingCooldown("트리플샷") > 0)
        {
            stateMachine.ChangeState(new PlayerRangeAttackState(stateMachine));
            return;
        }

        // Idle
        if(stateMachine.InputReader.MoveValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(RangeVelocity, 0f, DampTime, deltaTime);
            return;
        }

        // Moving
        stateMachine.Animator.SetFloat(RangeVelocity, 1f, DampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.RollEvent -= OnRolling; // 구르기
        stateMachine.Health.ImpactEvent -= OnImpact; // 피격 


        stateMachine.InputReader.AimingEvent -= OnAiming; // 정조준
        stateMachine.InputReader.SkillEvent -= OnSkill; // 화살비 
    }
    #endregion


    #region Main Methods
    private void Swap()
    {
        if (stateMachine.WeaponPrefabs[1].activeSelf)
        {
            stateMachine.WeaponPrefabs[0].SetActive(true);  // 근접 무기 활성화
            stateMachine.WeaponPrefabs[1].SetActive(false); // 원거리 무기 비활성화

            stateMachine.ChangeState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }
    #endregion


    #region Event Methods
    private void OnRolling() // 구르기
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
        return;
    }

    private void OnImpact() // 피격
    {
        if (stateMachine.Health.hitCount == 1)
        {
            stateMachine.ChangeState(new PlayerImpactState(stateMachine));
            return;
        }
    }

    private void OnAiming() // 정조준
    {
        if (stateMachine.CoolDownController.GetRemainingCooldown("정조준") <= 0f)
        {
            stateMachine.ChangeState(new PlayerRangeAimState(stateMachine));
            return;
        }
    }

    private void OnSkill() // 화살비
    {
        if(stateMachine.CoolDownController.GetRemainingCooldown("화살비") <= 0f)
        {
            stateMachine.ChangeState(new PlayerRangeSkyFallState(stateMachine));
            return;
        }
    }

    #endregion
}
