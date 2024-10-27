using UnityEngine;

public class PlayerRangeFreeLookState : PlayerBaseState
{
    public readonly int FreeLookWithRange = Animator.StringToHash("FreeLookWithRange"); // 원거리 블렌드 트리

    public readonly int RangeVelocity = Animator.StringToHash("RangeVelocity"); // 애니메이션 파라미터

    public readonly float CrossFadeDuration = 0.1f;

    public readonly float DampTime = 0.1f;

    public readonly float ExitTime = 0.8f;


    public PlayerRangeFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    #region abstarct Methods
    public override void Enter()
    {
        stateMachine.InputReader.RollEvent += OnRolling;
        stateMachine.InputReader.AimingEvent += OnAiming; // 정조준
        stateMachine.InputReader.SkillEvent += OnSkill; // 화살비

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookWithRange, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculatorMovement();

        Move(movement, deltaTime); // 이동

        Rotate(movement, deltaTime); // 회전

        if (Input.GetKeyDown(KeyCode.E)) { Swap(); }

        /*Debug.Log($"화살비 스킬 쿨타임: {SkillManager.instance.GetRemainingCooldown("화살비")}");*/

      

        // Attack
        if (stateMachine.InputReader.IsAttacking)
        {
            if (SkillManager.instance.IsPassiveActive("트리플샷")) // 트리플샷 [1]
            {
                stateMachine.ChangeState(new PlayerRangeRapidShotState(stateMachine));
                return;
            }
            else
            {
                stateMachine.ChangeState(new PlayerRangeAttackState(stateMachine));
                return;
            }
        }

        // Idle & Moving
        if (stateMachine.InputReader.MoveValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(RangeVelocity, 0f, DampTime, deltaTime);
            return;
        }
        else if(stateMachine.InputReader.MoveValue != Vector2.zero)
        {
            stateMachine.Animator.SetFloat(RangeVelocity, 1f, DampTime, deltaTime);
        }

    }

    public override void Exit()
    {
        stateMachine.InputReader.RollEvent -= OnRolling;
        stateMachine.InputReader.AimingEvent -= OnAiming;
        stateMachine.InputReader.SkillEvent -= OnSkill; 
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
    private void OnRolling()
    {
        stateMachine.ChangeState(new PlayerRollingState(stateMachine));
        return;
    }

    private void OnAiming() // 정조준 [0]
    {
        if (SkillManager.instance.GetRemainingCooldown("정조준") <= 0f && !DataManager.instance.playerData.skillData[0].isUnlock)
        {
            stateMachine.ChangeState(new PlayerRangeAimState(stateMachine));
            return;
        }
    }

    private void OnSkill() // 화살비 [2]
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        if (currentInfo.normalizedTime > 0.2f)
        {
            if (SkillManager.instance.GetRemainingCooldown("화살비") <= 0f && !DataManager.instance.playerData.skillData[2].isUnlock && stateMachine.InputReader.IsSkill)
            {
                Debug.Log("123");
                stateMachine.ChangeState(new PlayerRangeSkyFallState(stateMachine));
                return;
            }
        }

      
    }
    #endregion
}
