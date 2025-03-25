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
    [SerializeField] private int armor = 120;
    [SerializeField] private int shield = 200;

    [Header("Ui")]
    [SerializeField] private UnityEngine.UI.Slider healthBar;
    [SerializeField] private UnityEngine.UI.Image elementalIcon;
    [SerializeField] private string levelNumber;
    [SerializeField] private TextMeshProUGUI exarchonNamespace;

    private SpriteRenderer sprite;
    private Color spriteColor;

    private void OnValidate()
    {
        sprite = GetComponent<SpriteRenderer>();
        //sprite.color = FindFirstObjectByType<GameConfigManager>().GetElementalColor(myEnemyElement);
    }

    private void Start()
    {
        GetEnemyColor();
        initialHealth = health;
        ExarchonNaming();
        LevelAdaptation();
    }

    private void GetEnemyColor()
    {
        ////Change color using material instead
        ////sprite.color = GameConfigManager.Instance.GetElementalColor(myEnemyElement);
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

        if (health <= 0)
        { 
            health = 0;
            EnemyDeath();
        }
    }

    private void UpdateExarchonHpBar()
    {
        healthBar.value =  health/initialHealth;
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
