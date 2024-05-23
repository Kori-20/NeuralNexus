using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RarityColorPair
{
    public EItemRarity rarity;
    public Color color;
}

public class GameConfigManager : MonoBehaviour
{
    [SerializeField] private List<RarityColorPair> rarityColorPairs = new List<RarityColorPair>();
    private Dictionary<EItemRarity, Color> rarityMap = new Dictionary<EItemRarity, Color>();

    private void OnValidate()
    {
        rarityMap.Clear();
        foreach (var pair in rarityColorPairs)
        {
            rarityMap.Add(pair.rarity, pair.color);
        }
    }

    public Color GetRarityColor(EItemRarity rarity)
    {
        if (rarityMap.TryGetValue(rarity, out Color color)) return color;
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
