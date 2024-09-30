using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    public enum MonsterMovementControlType
    {
        PlayerControlled,
        AnimationDriven
    }


    public MonsterCombatAbility MonsterAbility => monsterAbility;
    public List<PointNode> Path => path;
    public Astar Astar => astar;
    public PointGrid PointGrid => pointGrid;

    private Animator animator;
    private MonsterMovementControlType movementType = MonsterMovementControlType.PlayerControlled;
    private MonsterCombatAbility monsterAbility;
    [SerializeField] private MonsterStatData monsterData;
    private Astar astar;
    private PointGrid pointGrid;
    private List<PointNode> path;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        astar = GetComponent<Astar>();
        pointGrid = FindObjectOfType<PointGrid>();    

        monsterAbility = new MonsterCombatAbility(monsterData);
    }

    private void Start()
    {
        if (astar.Path != null)
        {
            path = astar.Path;
        }
    }

    private void Update()
    {
        if (astar.HasTargetMoved)
        {
            path = astar.Path;
        }
    }

    // 이동 관련
    public float GetDistanceToTarget()
    {
        // target 여부 확인 및 거리 체크
        return 0;
    }

    public void SetMovementControl(MonsterMovementControlType moveType)
    {
        this.movementType = moveType;

        if (movementType == MonsterMovementControlType.AnimationDriven)
        {
            animator.applyRootMotion = true;
        }
        else
        {
            animator.applyRootMotion = false;
        }
    }

    // 전투 관련
    public void TakeDamage(int damage)
    {
        monsterAbility.Health -= damage;
    }
}