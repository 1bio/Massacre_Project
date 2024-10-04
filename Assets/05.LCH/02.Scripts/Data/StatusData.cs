[System.Serializable]
public class StatusData 
{
    public float moveSpeed;
    public float currentHealth;
    public float maxHealth;
    public float defense;

    public StatusData()
    {
        currentHealth = maxHealth;
    }
}
