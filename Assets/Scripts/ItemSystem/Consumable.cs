using UnityEngine;

public class Consumable : Item
{
    // Constructor that passes all parameters to the base class constructor
    public Consumable(int id, string name, string description, string icon, int stackSize, bool isStackable, bool isEquippable, bool isDiscardable, bool isConsumable, EItemRarity rarity)
        : base(id, name, description, icon, stackSize, isStackable, isEquippable, isDiscardable, isConsumable) // Fix: Pass isConsumable to the base constructor
    {
        this.isConsumable = isConsumable;
        this.rarity = rarity; // Fix: Assign rarity to a new field or property
    }

    public bool isConsumable { get; private set; }
    public EItemRarity rarity { get; private set; } // Fix: Add a new property for rarity
}
