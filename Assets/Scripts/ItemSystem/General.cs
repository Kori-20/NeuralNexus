using UnityEngine;

public class GeneralItem : Item
{
    // Constructor that passes all parameters to the base class constructor
    public GeneralItem(int id, string name, string description, EItemRarity rarity, Sprite icon,
                       int stackSize, bool isStackable, bool isEquippable, bool isDiscardable, bool isConsumable)
        : base(id, name, description, rarity, icon, stackSize, isStackable, isEquippable, isDiscardable, isConsumable)
    {
    }
}
