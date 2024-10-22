using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    // <==== Data ====>
    public PlayerData playerData;

    private StatusData statusData;
    private List<SkillData> skillData;
    private List<AttackData> attackData;
    private RangeAttackData rangeAttackData;
    

    // 데이터 초기화
    #region Initialized Methods
    private void Initialized()
    {
        statusData = new StatusData(5f, 100f, 50f);

        skillData = new List<SkillData>
        {
            // 원거리
            new SkillData("정조준", 0, 20f, 12f, 0f, 0f, true, "1초 동안 힘을 모아 \r\n강력한 화살을 발사합니다", 1.15f, 0.9f), // 스킬 1 [0]
            new SkillData("트리플샷", 0, 7f, 8f, 0f, 0f, true, "기본 공격 시 3발의 화살을 \r\n연속으로 발사합니다", 1.1f, 0.95f), // 스킬 2 [1]
            new SkillData("화살비", 0, 0f, 10f, 0f, 0f, true, "하늘에 떨어지는 화살을 \r\n여러 번 발사합니다", 1.05f, 0.95f), // 스킬 3 [2]

            // 근거리
            new SkillData("도약베기", 0, 20f, 10f, 0.02f, 0.35f, true, "짧게 도약하며 \r\n근처의 적들을 공격합니다", 1.07f, 0.85f), // 스킬 1 [3]
            new SkillData("화염칼", 0, 0f, 0f, 20f, 0.35f, true, "10초동안 근거리 무기를 \r\n불로 강화하여 공격합니다", 1.13f, 0.95f), // 스킬 1 [4]
            new SkillData("회전베기", 0, 20f, 9f, 0.005f, 0.35f, true, "1초동안 회전하며 \r\n강력한 힘을 방출합니다", 1.10f, 0.9f), // 스킬 3 [5]
        };
        
        attackData = new List<AttackData>()
        {
            new AttackData("Attack1@Melee", 1, 0.6f, 0.1f, 0.25f, 0.05f, 10f, 0f), // 근거리 공격 1
            new AttackData("Attack2@Melee", 2, 0.5f, 0.1f, 0.25f, 0.05f, 10f, 0f), // 근거리 공격 2
            new AttackData("Attack3@Melee", -1, 0, 0.1f, 0.25f, 0.05f, 20f, 0f), // 근거리 공격 3

            new AttackData("HeavyAttack1@Melee", 4, 0.6f, 0.1f, 0.25f, 0.35f, 20f, 0f), // 근거리 패시브 공격 1
            new AttackData("HeavyAttack2@Melee", 5, 0.5f, 0.1f, 0.25f, 0.35f, 20f, 0f), // 근거리 패시브 공격 2
            new AttackData("HeavyAttack3@Melee", -1, 0, 0.1f, 0.25f, 0.35f, 35f, 0f) // 근거리 패시브 공격 3
        };

        rangeAttackData = new RangeAttackData("None", 0, 0f, 0f, 0f, 0f, 10f, 0.3f); // 원거리 공격

        playerData = new PlayerData(statusData, skillData, attackData, rangeAttackData);
    }

    private void Awake()
    {
        instance = this;

        Initialized();
    }
    #endregion


    #region Main Methods
    // 스탯 레벨업
    public void StatusLevelUp(float moveSpeed = 0f, float maxHealth = 0f, float defense = 0f) 
    {
        StatusData player = playerData.statusData;

        player.moveSpeed += moveSpeed;
        player.maxHealth += maxHealth;
        player.defense += defense;
    }

    // 스킬 레벨 업
    public void SkillLevelUp(string skillName, int level)
    {
        foreach (SkillData skill in playerData.skillData)
        {
            if (skill.skillName != skillName)
                continue;

            skill.level += level;
            skill.damage *= skill.multipleDamage;
            skill.coolDown *= skill.multipleCoolDown;
            break; 
        }
    }
    #endregion


    #region Save & Load
    // Data -> Json
    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerData, true);
        string filePath = Path.Combine(Application.dataPath, "Resources", "playerData.json");

        File.WriteAllText(filePath, json);
    }

    // Json -> Data
    public void LoadData()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources", "playerData.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.LogWarning("저장된 게임 데이터가 없습니다");
        }
    }
    #endregion
}
