[System.Serializable]
public class StatusData 
{
    public float moveSpeed;
    public float currentHealth;
    public float maxHealth;
    public float defense;

    public StatusData(float moveSpeed, float maxHealth, float defense)
    {
        this.moveSpeed = moveSpeed;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.defense = defense;
    }
}
