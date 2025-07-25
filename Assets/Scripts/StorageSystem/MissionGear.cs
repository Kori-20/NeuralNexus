using System;
using UnityEngine;

public class MissionGear : MonoBehaviour
{
    //TODO: remove serielizefield from individual slots
    [Header("Guns")]
    private Gun[] gunSlots;
    [SerializeField] private Gun weapon1;
    [SerializeField] private Gun weapon2;
    [SerializeField] private Gun backupWeapon;
    //Hardcoded gun IDs for now
    [SerializeField] private int gunID1;
    [SerializeField] private int gunID2;
    [SerializeField] private int gunID3;

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
        //Load all CSV data on game awake for now
        //TODO: Make data load persistent so that it only loads once every update
        CSVReader.LoadAllCSVData();
        SetSlotRefs();
    }

    private void Start()
    {
        SetUi();
    }

    private void SetSlotRefs()
    {
        
        weapon1 = GetAndCastGun(gunID1);
        weapon2 = GetAndCastGun(gunID2);
        backupWeapon = GetAndCastGun(gunID3);

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


    private Gun GetAndCastGun(int itemID)
    {
        Item item = CSVReader.GetItem(itemID) ?? throw new ArgumentNullException(nameof(itemID), $"No item found with ID {itemID}.");

        if (item is Gun originalGun)
        {
            return new Gun(
                originalGun.ID,
                originalGun.Name,
                originalGun.FullName,
                originalGun.Icon,
                originalGun.Description,
                originalGun.Damage,
                originalGun.FireRate,
                originalGun.MagazineSize,
                originalGun.Accuracy,
                originalGun.ReloadSpeed,
                originalGun.WeaponType,
                originalGun.PhysicalType,
                originalGun.IsAutomatic,
                originalGun.PelletCount
            );
        }

        Debug.LogWarning($"The item with ID {itemID} is not a Gun. It is a {item.GetType().Name}.");
        return null;
    }



    private void SetUi()
    {
        ///Change the color with color of rarity of item being used
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
