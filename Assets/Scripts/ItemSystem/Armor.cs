using UnityEngine;

public class Armor : Equipment
{
    public int Defense { get; private set; }

    public Armor(int id, string name, string description, EItemRarity rarity, string uniqueEffect, string nonAbbreviatedName, float durability, float maxDurability, int defense, Sprite icon)
        : base(id, name, description, rarity, icon, uniqueEffect, nonAbbreviatedName, durability, maxDurability)
    {
        Defense = defense;
    }
}
