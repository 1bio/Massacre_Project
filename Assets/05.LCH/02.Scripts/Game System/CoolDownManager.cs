using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownManager : MonoBehaviour
{
    private Dictionary<string, float> skills = new Dictionary<string, float>();

    // ��Ÿ�� ����
    public void StartCooldown(string skillName, float cooldownDuration)
    {
        if (skills.ContainsKey(skillName))
        {
            skills[skillName] = Time.time + cooldownDuration;
        }
        else
        {
            skills.Add(skillName, Time.time + cooldownDuration);
        }
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
}
