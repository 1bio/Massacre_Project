using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownController : MonoBehaviour
{
    private Dictionary<string, float> skills = new Dictionary<string, float>();

    // 스킬 추가
    public void AddSkill(string skillName, float cooldownDuration)
    {
        skills.Add(skillName, cooldownDuration);
    }

    // 쿨타임 시작
    public void StartCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName] = Time.time + ReturnCoolDown(skillName);
        }
    }
 
    // 남은 스킬 시간 반환
    public float GetRemainingCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            float remainingTime = skills[skillName] - Time.time;
            return Mathf.Max(remainingTime, 0); 
        }
        return 0; 
    }

    // 스킬 쿨타임 반환
    public float ReturnCoolDown(string skillName)
    {
        foreach (SkillData skill in DataManager.instance.playerData.skillData)
        {
            if(skill.skillName == skillName)
            {
                return skill.coolDown;
            }
        }

        return 0;
    }

    // 스킬 사용 가능 여부 
    public bool IsSkillOnCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            return Time.time < skills[skillName];
        }
        return false;
    }

}
