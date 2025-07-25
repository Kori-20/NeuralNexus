using UnityEngine;

public class Item
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Icon { get; private set; }
    public int StackSize { get; private set; }
    public bool IsStackable { get; private set; }
    public bool IsEquippable { get; private set; }
    public bool IsDiscardable { get; private set; }
    public bool IsConsumable { get; private set; }

    public Item(int id, string name, string description,string iconPath,
        int stackSize, bool isStackable, bool isEquippable, bool isDiscardable, bool isConsumable)
    {
        ID = id;
        Name = name;
        Description = description;
        Icon = iconPath;
        StackSize = stackSize;
        IsStackable = isStackable;
        IsEquippable = isEquippable;
        IsDiscardable = isDiscardable;
        IsConsumable = isConsumable;
    }
}
