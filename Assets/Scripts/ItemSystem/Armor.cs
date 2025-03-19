public class Armor : Equipment
{
    public int Defense { get; private set; }

    public Armor(int id, string name, string description, EItemRarity rarity, string uniqueEffect, string nonAbbreviatedName, float durability, float maxDurability, int defense)
        : base(id, name, description, rarity, uniqueEffect, nonAbbreviatedName, durability, maxDurability)
    {
        Defense = defense;
    }
}
