using UnityEngine;

public class Equipment : Item
{
    public string UniqueEffect { get; private set; }
    public string NonAbbreviatedName { get; private set; }
    public float Durability { get; private set; }
    public float MaxDurability { get; private set; }

    public Equipment(int id, string name, string description, EItemRarity rarity, Sprite icon, string uniqueEffect,
                     string nonAbbreviatedName, float durability, float maxDurability)
        : base(id, name, description, rarity, icon, stackSize: 1, isStackable: false, isEquippable: true, isDiscardable: true, isConsumable: false)
    {
        UniqueEffect = uniqueEffect;
        NonAbbreviatedName = nonAbbreviatedName;
        Durability = durability;
        MaxDurability = maxDurability;
    }
}
