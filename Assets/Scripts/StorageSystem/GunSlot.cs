using UnityEngine;

[CreateAssetMenu(fileName = "New Storage Slot", menuName = "Storage/Gun Slot")]
public class GunSlot : StorageSlot
{
    [SerializeField] public Gun gunInSlot;
}
