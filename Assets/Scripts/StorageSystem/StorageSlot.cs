using UnityEngine;

[CreateAssetMenu(fileName = "New Storage Slot", menuName = "Storage/Slot")]
public class StorageSlot : ScriptableObject
{
    [SerializeField] private Item itemInSlot;
    [SerializeField] private int itemCount;
    [SerializeField] private int slotIndex;
    [SerializeField] private Sprite slotBorder;
}
