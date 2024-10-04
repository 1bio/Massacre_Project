using System.Collections.Generic;

[System.Serializable]
public class PlayerData 
{
    public StatusData statusData;
    public List<SkillData> skillData;

    public List<AttackData> attackData;


    public PlayerData(StatusData statusData, List<SkillData> skillData, List<AttackData> attackData)
    {
        this.statusData = statusData;
        this.skillData = skillData;
        this.attackData = attackData;
    }
}
