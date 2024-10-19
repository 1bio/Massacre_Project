using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownController : MonoBehaviour
{
    private Dictionary<string, float> skills = new Dictionary<string, float>();

    // ��ų �߰�
    public void AddSkill(string skillName, float cooldownDuration)
    {
        skills.Add(skillName, cooldownDuration);
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

    // ��ų ��Ÿ�� ��ȯ
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

    // ��ų ��� ���� ���� 
    public bool IsSkillOnCooldown(string skillName)
    {
        if (skills.ContainsKey(skillName))
        {
            return Time.time < skills[skillName];
        }
        return false;
    }

}
