using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    //public float atkBonus;
    //public float defenceBonus;
    ////스탯 추가 가능

    private void Reset()
    {
        type = ItemType.Equipment;
    }

}
