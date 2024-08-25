//Base class for all weapons both on player side and for the enemies
//Exclusivity of weapons is defined by lack of implementation of certain weapons in the player or enemy class
using UnityEngine;
enum EWeaponType
{
    AssaultRifle,
    Shotgun,
    SniperRifle,
    RocketLauncher,
    GrenadeLauncher,
    SubmachineGun,
    HeavyMachineGun,
    DualPistol,
    Flamethrower,
    Railgun,
    AcidSprayer,
    HarpoonGun,
    LaserLens,
    PlasmaCoil,
    //Player Exclusive
    Pistol,
    //Enemy Exclusive
    SuicideBomb,
    QuadPistol,
    ElectromagneticPulse,
    DebrisLauncher
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Equipment/Gun")]
public class Gun : Equipment
{
    [Header("Guns")]
    [SerializeField] private float damage = 10;
    [SerializeField] public float fireRate = 2;
    [SerializeField] private float range;
    [SerializeField] public float accuracy = 70;
    [SerializeField] public float recoil = 0.1f;
    [SerializeField] public int pelletsPerShot = 1;

    [SerializeField] public bool isAutomatic = true;

    [SerializeField] public float reloadTime = 1.5f;
    [SerializeField] public int magazineSize = 20;
    [SerializeField] public int magsStored = 22;

    [SerializeField] private EWeaponType weaponType;

    [Header("Changeable Stats")]
    [SerializeField] public int currentAmmo;
    [SerializeField] public int currentMags;

    private void OnValidate()
    {
        currentAmmo = magazineSize;
        currentMags = magsStored;
    }

    private void Awake()
    {
        currentAmmo = magazineSize;
        currentMags = magsStored;
    }

    public virtual void SpawnWeaponProjetile(Vector3 directionPostSpread)//This method will be overriden by the child classes
    {
        
    }  
}
