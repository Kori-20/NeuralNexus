using UnityEngine;

public class Gun : Equipment
{
    public int Damage { get; private set; }
    public float FireRate { get; private set; }
    public int Range { get; private set; }
    public int Accuracy { get; private set; }
    public int Velocity { get; private set; }
    public float Recoil { get; private set; }
    public int PelletsPerShot { get; private set; }
    public bool IsAutomatic { get; private set; }

    public float ReloadTime { get; private set; }
    public int MagazineSize { get; private set; }
    public int MagsStored { get; private set; }

    public EWeaponType WeaponType { get; private set; }
    public EPhysicalDamageType PhysicalType { get; private set; }
    public EElementalDamageType ElementType { get; private set; }

    public int CurrentAmmo { get; private set; }
    public int CurrentMags { get; private set; }

    public Gun(int id, string name, string description, EItemRarity rarity, Sprite icon,
               string uniqueEffect, string nonAbbreviatedName, float durability, float maxDurability,
               int damage, float fireRate, int range, int accuracy, int velocity, float recoil, int pelletsPerShot,
               bool isAutomatic, float reloadTime, int magazineSize, int magsStored,
               EWeaponType weaponType, EPhysicalDamageType physicalType, EElementalDamageType elementType)
        : base(id, name, description, rarity, icon, uniqueEffect, nonAbbreviatedName, durability, maxDurability)
    {
        Damage = damage;
        FireRate = fireRate;
        Range = range;
        Accuracy = accuracy;
        Velocity = velocity;
        Recoil = recoil;
        PelletsPerShot = pelletsPerShot;
        IsAutomatic = isAutomatic;

        ReloadTime = reloadTime;
        MagazineSize = magazineSize;
        MagsStored = magsStored;

        WeaponType = weaponType;
        PhysicalType = physicalType;
        ElementType = elementType;

        CurrentAmmo = magazineSize;
        CurrentMags = magsStored;
    }
}
