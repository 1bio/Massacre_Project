using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    //public float atkBonus;
    //public float defenceBonus;
    ////���� �߰� ����

    private void Reset()
    {
        type = ItemType.Equipment;
    }

}
