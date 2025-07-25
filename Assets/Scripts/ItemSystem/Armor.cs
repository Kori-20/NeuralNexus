using UnityEngine;

public class Armor : Equipment
{
    public int Defense { get; private set; }

    public Armor(int id, string name, string description, EItemRarity rarity, int defense, string icon)
        : base(id, name, description, icon)
    {
        Defense = defense;
    }
}
