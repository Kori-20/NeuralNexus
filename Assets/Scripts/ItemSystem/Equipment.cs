using UnityEngine;

public class Equipment : Item
{

    public Equipment(int id, string name, string description, Sprite icon)
    : base(id, name, description, icon, stackSize: 1, isStackable: false, isEquippable: true, isDiscardable: true, isConsumable: false)
    {
 
    }
}
