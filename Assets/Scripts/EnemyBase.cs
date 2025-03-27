using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Exarchon")]
    [SerializeField] private EExarchonCatergory exarchonCatergory;
    [SerializeField] private string exarchonCodeName = "Ex000A";
    [SerializeField] private EElementalDamageType myEnemyElement;
    [SerializeField] private EWeaponType exarchonWeapon;

    [Header("Stats")]
    [SerializeField] private float health = 500;
    private float initialHealth;
    [SerializeField] private int damage = 10;
    [SerializeField] private float armor = 120;
    [SerializeField] private float shield = 200;
    private float healthPool;

    [Header("Ui")]
    [SerializeField] private UnityEngine.UI.Slider healthBar;
    [SerializeField] private UnityEngine.UI.Image healthBarFill;
    [SerializeField] private UnityEngine.UI.Image elementalIcon;
    [SerializeField] private string levelNumber;
    [SerializeField] private TextMeshProUGUI exarchonNamespace;

    private void Start()
    {
        healthPool = initialHealth = health + armor + shield;
        //foreach (Transform child in transform) child.gameObject.SetActive(true);
        HpBarColoring();
        ExarchonNaming();
        LevelAdaptation();
    }

    private void LevelAdaptation()
    {
        //Change the level of the exarchon based on the zone level ()
    }

    void IDamageable.CalculateDamage(int damage, EPhysicalDamageType physicalType, EElementalDamageType elementType)
    {
        int calcDmg = (Random.Range(80,120) * damage)/100; //change damage by -20% to +20% [80-120%]

        calcDmg = (int)ElementMath(calcDmg, elementType);//Damage change based on element

        if (shield > 0)
        {
            ShieldMath(calcDmg, physicalType); //Deals damage to shield
        }
        else 
        {
            
            if (health > 0)
            {
                HealthMath(calcDmg, physicalType); //Deals damage to health or armored health
            }

            if (armor > 0)
            {
                ArmorMath(calcDmg, physicalType); //Deals damage to armor
            }
        }
    }

    private float ElementMath(int damage, EElementalDamageType gunElemType)
    {
        if (DamageModifier.ElementalDamageMultiplier.TryGetValue((gunElemType, myEnemyElement), out float multiplier))
        {
            return damage * multiplier;
        }
        return damage;
    }

    private void ShieldMath(int damage, EPhysicalDamageType physicalType) 
    {
        if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.Shield), out float multiplier)) { }
        else multiplier = 1.0f;
        int shieldDamage = (int)(damage * multiplier);
        shield -= shieldDamage;

        InGameUiManager.Instance.SpawnFloatingNumbers(transform.position, shieldDamage, Quaternion.identity, Color.blue, gameObject);

        UpdateExarchonHpBar();
        if (shield < 0) shield = 0;
    }

    private void ArmorMath(int damage, EPhysicalDamageType physicalType)
    {
        if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.Armor), out float multiplier)) { }
        else multiplier = 1.0f;
        int armorDamage = (int)(damage * multiplier);
        armor -= armorDamage;

        if (armorDamage > 0)
        {
            InGameUiManager.Instance.SpawnFloatingNumbers(transform.position, armorDamage, Quaternion.identity, Color.yellow, gameObject);
        }

        UpdateExarchonHpBar();
        if (armor < 0) armor = 0;
    }

    private void HealthMath(int damage, EPhysicalDamageType physicalType)
    {
        if (armor > 0)
        {
            if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.ArmoredHealth), out float multiplier)) { }
            else multiplier = 1.0f;
            int armoredHealthDamage = (int)(damage * multiplier);
            health -= armoredHealthDamage;
            InGameUiManager.Instance.SpawnFloatingNumbers(transform.position, armoredHealthDamage, Quaternion.identity, Color.gray, gameObject);
        }
        else
        {
            if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.Health), out float multiplier)) { }
            else multiplier = 1.0f;
            int healthDamage = (int)(damage * multiplier);
            health -= healthDamage;
            InGameUiManager.Instance.SpawnFloatingNumbers(transform.position, healthDamage, Quaternion.identity, Color.green, gameObject);
        }

        UpdateExarchonHpBar();
        if (health <= 0) { health = 0; EnemyDeath(); }
    }

    private void UpdateExarchonHpBar()
    {
        //Health pool is calculated every damage instance so that mechanics using hp/shield/armor% can be used
        healthPool = health + armor + shield;
        HpBarColoring();
        healthBar.value =  healthPool/initialHealth;
        // healthBarFill.color = ;
    }

    private void HpBarColoring()
    {
        //Run once at start to set the color of the health bar && //Run every time the health bar is updated to change the color of the health bar
        if (shield > 0)
        {
            healthBarFill.color = Color.blue;
        }
        else if (armor > 0)
        {
            healthBarFill.color = Color.gray;
        }
        else
        {
            healthBarFill.color = Color.red;
        }
    }

    private void ExarchonNaming()
    {
        string category = exarchonCatergory.ToString();
        string weapon = GetAbbreviation(exarchonWeapon);
        string digits = Random.Range(0, 1000).ToString();
        string letter = ((char)Random.Range('A', 'Z' + 1)).ToString();
        exarchonCodeName = category + " " + weapon + digits + letter;

        exarchonNamespace.text = exarchonCodeName;
    }

    private static readonly Dictionary<EWeaponType, string> WeaponAbbreviationMap = new Dictionary<EWeaponType, string>
    {
        { EWeaponType.AssaultRifle, "AR" },
        { EWeaponType.Shotgun, "SG" },
        { EWeaponType.RocketLauncher, "RL" },
        { EWeaponType.GrenadeLauncher, "GL" },
        { EWeaponType.SubmachineGun, "SMG" },
        { EWeaponType.HeavyMachineGun, "HMG" },
        { EWeaponType.DualPistol, "DP" },
        { EWeaponType.Pistol, "P" },
    };

    public static string GetAbbreviation(EWeaponType weaponType)
    {
        return WeaponAbbreviationMap.TryGetValue(weaponType, out string abbreviation) ? abbreviation : "??";
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
