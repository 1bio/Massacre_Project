using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSkillData : ScriptableObject
{
    [Header("Animation Name")]
    [SerializeField] private string _animationName;

    [Header(" # VFX")]
    [SerializeField] private GameObject[] _vfx;

    [Header(" # Cooltime")]
    [SerializeField] private float _cooldownThreshold;

    [Header(" # Damage")]
    [SerializeField] private float _damage;

    [Header(" # Range")]
    [SerializeField] private float _range;

    [Header(" # Cast Time")]
    [SerializeField] private float _castTime;

    public string AnimationName { get => _animationName; }
    public GameObject[] VFX { get => _vfx; }
    public float CooldownThreshold { get => _cooldownThreshold; }
    public float CooldownTimer { get; set; } = 0f;
    public float Damage { get => _damage; }
    public float Range { get => _range; }
    public float CastTime { get => _castTime; }

    public Indicator Indicator { get; set; }

    public abstract void ActiveSkillEnter(Monster monster);
    public abstract void ActiveSkillTick(Monster monster);
    public abstract void ActiveSkillExit(Monster monster);

    public MonsterSkillData CreateInstance()
    {
        return Instantiate(this);
    }
}
