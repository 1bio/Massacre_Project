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
        statusData = new StatusData()
        {
            moveSpeed = 7f,
            maxHealth = 100f,
            defense = 0f
        };

        skillData = new List<SkillData>
        {
            new SkillData("차징샷", 1, 15f, 5f), // 스킬 1
            new SkillData("연발사격", 1, 7f, 5f), // 스킬 2
            new SkillData("폭탄화살", 1, 20f, 4f) // 스킬 3
        };
        
        attackData = new List<AttackData>()
        {
            new AttackData("Attack1@Melee", 1, 0.6f, 0.1f, 0.75f, 0.35f, 10.0f, 0.5f), // 근거리 공격 1
            new AttackData("Attack2@Melee", 2, 0.5f, 0.1f, 0.5f, 0.35f, 15f, 0.5f), // 근거리 공격 2
            new AttackData("Attack3@Melee", -1, 0, 0.1f, 0.75f, 0.35f, 20.0f, 0.7f) // 근거리 공격 3
        };

        rangeAttackData = new RangeAttackData("None", 0, 0f, 0f, 0f, 0f, 10f, 0.3f);

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
    public void SkillLevelUp(int level, float MultiplierDamage, float MultiplierCoolDown)
    {
        foreach (SkillData skill in playerData.skillData)
        {
            skill.level += level;
            skill.damage *= MultiplierDamage;
            skill.coolDown *= MultiplierCoolDown;
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
