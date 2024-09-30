using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RarityColorPair
{
    public EItemRarity rarity;
    public Color color;
}

[Serializable]
public struct ElementalColorPair
{
    public EElementalDamageType elementType;
    public Color color;
}


public class GameConfigManager : MonoBehaviour
{
    [SerializeField] private List<RarityColorPair> rarityColorPairs = new List<RarityColorPair>();
    private Dictionary<EItemRarity, Color> rarityMap = new Dictionary<EItemRarity, Color>();

    [SerializeField] private List<ElementalColorPair> elementalColorPairs = new List<ElementalColorPair>();
    private Dictionary<EElementalDamageType, Color> elementalMap = new Dictionary<EElementalDamageType, Color>();

    private void OnValidate()
    {
        rarityMap.Clear();
        foreach (var pair in rarityColorPairs)
        {
            rarityMap.Add(pair.rarity, pair.color);
        }

        elementalMap.Clear();
        foreach (var pair in elementalColorPairs)
        {
            elementalMap.Add(pair.elementType, pair.color);
        }
    }

    public Color GetRarityColor(EItemRarity rarity)
    {
        if (rarityMap.TryGetValue(rarity, out Color color)) return color;
        else return Color.white;
    }

    public Color GetElementalColor(EElementalDamageType elementType)
    {
        if (elementalMap.TryGetValue(elementType, out Color color)) return color;
        else return Color.white;
    }
}

public enum EItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic,
    Nexus
}

public enum EWeaponType
{
    AssaultRifle,
    Shotgun,
    SniperRifle,
    RocketLauncher,
    GrenadeLauncher,
    SubmachineGun,
    HeavyMachineGun,
    DualPistol,
    Pistol,
}

public enum EPlayerMotion
{
    Shoot,
    Cover,
    Transit,
}

public enum EAbilityKey
{
    Q,
    W,
    E,
}

public enum EPhysicalDamageType
{
    Blunt,
    Piercing,
    Explosive,
    Energetic,
}

public enum EElementalDamageType
{
    Neutral,
    Crystal,
    Radiation,
    Magma,
    Plasma,
    Sound,
    Void,
}


public enum EDefenseStats
{
    Health,
    Armor,
    Shield,
    ArmoredHealth,
}

public enum EVFXClassification
{
    PermaFX,
    EnemyFX,
    PlayerFX
}