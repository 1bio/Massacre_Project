using TMPro;

[System.Serializable]
public class UIData
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI coolDownText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI multipleDamage;
    public TextMeshProUGUI multipleCoolDown;

    public UIData(TextMeshProUGUI nameText, TextMeshProUGUI level,TextMeshProUGUI cooldown, TextMeshProUGUI descriptionText, TextMeshProUGUI multipleDamage, TextMeshProUGUI multipleCoolDown)
    {
        this.levelText = level;
        this.nameText = nameText;
        this.coolDownText = cooldown;
        this.descriptionText = descriptionText;
        this.multipleDamage = multipleDamage;
        this.multipleCoolDown = multipleCoolDown;
    }
}
