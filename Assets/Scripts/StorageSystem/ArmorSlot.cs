using UnityEngine;

[CreateAssetMenu(fileName = "New Storage Slot", menuName = "Storage/Armor Slot")]
public class ArmorSlot : StorageSlot
{
    [SerializeField] public Armor armorInSlot;
}
