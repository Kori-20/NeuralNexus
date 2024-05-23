using UnityEngine;

[CreateAssetMenu(fileName = "New Storage Slot", menuName = "Storage/Ability Slot")]
public class AbilitySlot : StorageSlot
{
    [SerializeField] public Ability abilityInSlot;
}
