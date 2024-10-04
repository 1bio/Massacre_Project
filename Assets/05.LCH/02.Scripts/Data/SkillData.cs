[System.Serializable]
public class SkillData 
{
    public string skillName;
    public int level;
    public float damage;
    public float coolDown;

    public SkillData(string skillName, int level, float damage, float coolDown)
    {
        this.skillName = skillName;
        this.level = level;
        this.damage = damage;
        this.coolDown = coolDown;
    }
}
