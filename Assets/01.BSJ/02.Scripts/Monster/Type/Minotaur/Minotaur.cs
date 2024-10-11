using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Monster
{
    [Header(" # Skill Data")]
    [SerializeField] private MonsterSkillData _monsterSkillData;

    protected override void Awake()
    {
        base.Awake();

        MonsterCombatController = new MonsterCombatController(p_monsterStatData, _monsterSkillData, GetComponent<Health>());
    }

    public enum RamAttackAnimationName
    {
        RamStart,
        RamRun,
        RamAttack,
        RamWall
    }
}
