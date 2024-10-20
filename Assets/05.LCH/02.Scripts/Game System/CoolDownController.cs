using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownController : MonoBehaviour
{
    private Dictionary<string, float> skills = new Dictionary<string, float>();
    private Dictionary<string, float> passiveTimer = new Dictionary<string, float>();

    // 스킬 추가
    public void AddSkill(string skillName, float cooldownDuration)
    {
        skills.Add(skillName, cooldownDuration);
    }

    // 패시브 추가
    public void AddPassiveSkill(string skillName, float cooldownDuration)
    {
        skills.Add(skillName, Time.time + cooldownDuration);
    }

    // 등록된 스킬의 쿨타임(데이터) 반환
    public float ReturnCoolDown(string skillName)
    {
        foreach (SkillData skill in DataManager.instance.playerData.skillData)
        {
            if (skill.skillName == skillName)
            {
                return skill.coolDown;
            }
        }

        return 0;
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

    // 남은 패시브 스킬 시간 반환
    public float GetRemainingPassiveCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            float remainingTime = passiveTimer[skillName] - Time.time;
            return Mathf.Max(remainingTime, 0);
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

    // 패시브 지속 시간 반환
    public bool IsPassiveOn(string skillName)
    {
        if (passiveTimer.ContainsKey(skillName))
        {
            float remainingTime = passiveTimer[skillName] - Time.time;
            return remainingTime > 0;
        }

        return false;
    }



    private void Update()
    {
        List<string> finishedPassives = new List<string>();

        // 종료된 패시브 리스트에 추가
        foreach (var passive in passiveTimer)
        {
            if(Time.time >= passive.Value)
            {
                finishedPassives.Add(passive.Key);
            }
        }

        // 지속 시간 후 쿨타임 시작
        foreach (string skillName in finishedPassives)
        {
            StartCooldown(skillName);
            passiveTimer.Remove(skillName);
        }
    }
}
