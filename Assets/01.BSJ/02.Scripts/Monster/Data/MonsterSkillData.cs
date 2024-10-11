using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSkillData : ScriptableObject
{
    [Header(" # VFX")]
    public GameObject[] VFX;

    [Header(" # Cooltime")]
    public float CooldownThreshold;

    [Header(" # Damage")]
    public float Damage;

    [Header(" # Range")]
    public float Range;

    [Header(" # Duration")]
    public float Duration;

    [Header(" # Cast Time")]
    public float CastTime;

    public abstract void ActiveSkillEnter(Monster monster);
    public abstract void ActiveSkillTick(Monster monster);
    public abstract void ActiveSkillExit(Monster monster);

    public MonsterSkillData CreateInstance()
    {
        return Instantiate(this);
    }
}
