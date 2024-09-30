using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Equipment")]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public string ItemName;
    public Sprite ItemIcon;
    public string Description;
  
}

public enum ItemType { Helmet, Armor, Pants, Gloves, Boots, Melee, Range }

