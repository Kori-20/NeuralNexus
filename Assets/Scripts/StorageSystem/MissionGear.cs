using Unity.VisualScripting;
using UnityEngine;

public class MissionGear : MonoBehaviour
{
    //TODO: remove serielizefield from individual slots
    [Header("Guns")]
    private Gun[] gunSlots;
    [SerializeField] private Gun weapon1;
    [SerializeField] private Gun weapon2;
    [SerializeField] private Gun backupWeapon;

    [Header("Armor")]
    private Armor[] armorSlots;
    [SerializeField] private Armor helmet;
    [SerializeField] private Armor vest;
    [SerializeField] private Armor gloves;
    [SerializeField] private Armor tacticalPants;
    [SerializeField] private Armor kneePads;
    [SerializeField] private Armor boots;

    [Header("Abilities")]
    private Ability[] abilitySlots;
    [SerializeField] private Ability ability01;
    [SerializeField] private Ability ability02;
    [SerializeField] private Ability ability03;

    private void Awake()
    {
        SetSlotRefs();
    }

    private void Start()
    {
        SetUi();
    }

    private void SetSlotRefs()
    {
        gunSlots = new Gun[3];
        gunSlots[0] = weapon1;
        gunSlots[1] = weapon2;
        gunSlots[2] = backupWeapon;

        armorSlots = new Armor[6];
        armorSlots[0] = helmet;
        armorSlots[1] = vest;
        armorSlots[2] = gloves;
        armorSlots[3] = tacticalPants;
        armorSlots[4] = kneePads;
        armorSlots[5] = boots;

        abilitySlots = new Ability[3];
        abilitySlots[0] = ability01;
        abilitySlots[1] = ability02;
        abilitySlots[2] = ability03;
    }

    private void SetUi()
    {
        ///Change the color 
        if (weapon1 != null) InGameUiManager.Instance.SyncGunIcons(0, weapon1.Icon, Color.blue);
        if (weapon2 != null) InGameUiManager.Instance.SyncGunIcons(1, weapon2.Icon, Color.blue);
        if (backupWeapon != null) InGameUiManager.Instance.SyncGunIcons(2, backupWeapon.Icon, Color.blue);
    }

    private void OnMissionStart()
    {

    }

    #region Equip & Unequip
    private void EquipWeapon(int slot, Gun gun)
    {
        gunSlots[slot] = gun;
    }

    private void UnequipWeapon(int slot)
    {
        gunSlots[slot] = null;
    }

    private void EquipArmorPiece(int slot, Armor armor)
    {
        armorSlots[slot] = armor;
    }

    private void UnequipArmorPiece(int slot)
    {
        armorSlots[slot] = null;
    }

    private void EquipAbility(int slot, Ability ability)
    {
        abilitySlots[slot] = ability;
    }

    private void UnequipAbility(int slot)
    {
        abilitySlots[slot] = null;
    }
    #endregion

    #region Getters & Setters

    public Gun GetGunInSlot(int slot)
    {
        return gunSlots[slot];
    }

    #endregion
}
