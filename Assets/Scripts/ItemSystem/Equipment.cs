using UnityEngine;

public class Equipment : Item
{
    public Equipment(int id, string name, string description, string iconPath)
    : base(id, name, description, iconPath, stackSize: 1, isStackable: false, isEquippable: true, isDiscardable: true, isConsumable: false)
    {
        
    }
}
