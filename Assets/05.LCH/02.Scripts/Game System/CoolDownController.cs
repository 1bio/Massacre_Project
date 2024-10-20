using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownController : MonoBehaviour
{
    private Dictionary<string, float> skills = new Dictionary<string, float>();
    private Dictionary<string, float> passiveTimer = new Dictionary<string, float>();

    // ��ų �߰�
    public void AddSkill(string skillName, float cooldownDuration)
    {
        skills.Add(skillName, cooldownDuration);
    }

    // �нú� �߰�
    public void AddPassiveSkill(string skillName, float cooldownDuration)
    {
        skills.Add(skillName, Time.time + cooldownDuration);
    }

    // ��ϵ� ��ų�� ��Ÿ��(������) ��ȯ
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

    // ��Ÿ�� ����
    public void StartCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName] = Time.time + ReturnCoolDown(skillName);
        }
    }
 
    // ���� ��ų �ð� ��ȯ
    public float GetRemainingCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            float remainingTime = skills[skillName] - Time.time;
            return Mathf.Max(remainingTime, 0); 
        }
        return 0; 
    }

    // ���� �нú� ��ų �ð� ��ȯ
    public float GetRemainingPassiveCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            float remainingTime = passiveTimer[skillName] - Time.time;
            return Mathf.Max(remainingTime, 0);
        }
        return 0;
    }

    // ��ų ��� ���� ���� 
    public bool IsSkillOnCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            return Time.time < skills[skillName];
        }
        return false;
    }

    // �нú� ���� �ð� ��ȯ
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

        // ����� �нú� ����Ʈ�� �߰�
        foreach (var passive in passiveTimer)
        {
            if(Time.time >= passive.Value)
            {
                finishedPassives.Add(passive.Key);
            }
        }

        // ���� �ð� �� ��Ÿ�� ����
        foreach (string skillName in finishedPassives)
        {
            StartCooldown(skillName);
            passiveTimer.Remove(skillName);
        }
    }
}
