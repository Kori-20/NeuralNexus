using Unity.Collections;
using UnityEngine;

public class Equipment : Item
{
    [Header("Equipment")]
    [SerializeField, TextArea(2, 10)] protected string uniqueEffect;
    [SerializeField] protected string nonAbriviatedName ;
    [SerializeField] protected float durability;
    [SerializeField,ReadOnly] protected float maxDurability;

    public Equipment()
    {
        isDiscardable = true;
        isEquippable = true;
        isConsumable = false;
        isStackable = false;
    }
}
