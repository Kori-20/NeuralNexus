using UnityEngine;

public class Gun : Equipment
{
    public string FullName { get; private set; }
    public float Damage { get; private set; }
    public float FireRate { get; private set; }
    public int Range { get; private set; }
    public int Accuracy { get; private set; }
    public int Velocity { get; private set; }
    public float ReloadSpeed { get; private set; }
    public int MagazineSize { get; private set; }
    public EWeaponType WeaponType { get; private set; }
    public EPhysicalDamageType PhysicalType { get; private set; }
    public bool IsAutomatic { get; private set; }
    public int CurrentAmmo { get; set; }
    public int PelletCount { get; private set; }

    public Gun(int id, string name, string fullname, string iconPath, string description, float damage, float fireRate, int magazineSize, int accuracy, float reloadSpeed, EWeaponType type, EPhysicalDamageType p_type, bool isAutomatic, int pelletCount)
        : base(id, name, description, iconPath)
    {
        FullName = fullname;
        Damage = damage;
        FireRate = fireRate;
        Accuracy = accuracy;
        ReloadSpeed = reloadSpeed;
        MagazineSize = magazineSize;
        WeaponType = type;
        PhysicalType = p_type;
        IsAutomatic = isAutomatic;
        PelletCount = pelletCount;

        //runtime stats
        CurrentAmmo = magazineSize;
    }
}
