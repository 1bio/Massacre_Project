[System.Serializable]
public class SkillData 
{
    public string skillName;
    public int level;
    public float damage;
    public float coolDown;
    public float force;
    public float forceTime;
    public bool isUnlock;
    public string description;
    public float multipleDamage;
    public float multipleCoolDown;
    public float duration;
    public bool isPassive;

    public SkillData(string skillName, int level, float damage, float coolDown, float force, float forceTime, bool isUnlock, string description, float multipleDamage, float multipleCoolDown, float duration, bool isPassive)
    {
        this.skillName = skillName;
        this.level = level;
        this.damage = damage;
        this.coolDown = coolDown;
        this.force = force;
        this.forceTime = forceTime;
        this.isUnlock = isUnlock;
        this.description = description;
        this.multipleDamage = multipleDamage;
        this.multipleCoolDown = multipleCoolDown;
        this.duration = duration;
        this.isPassive = isPassive;
    }
}
