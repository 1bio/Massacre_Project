using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSkillData", menuName = "Data/MonsterSKillData")]
public class MonsterSkillData : ScriptableObject
{
    public GameObject[] vfx;

    public float coolTime;

    public float damage;

    public string animationName;

    public float range;

    public float duration;

    public float castTime;
}
