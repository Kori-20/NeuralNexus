using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RarityColorPair
{
    public EItemRarity rarity;
    public Color color;
}

[CreateAssetMenu(fileName = "NewRarityList", menuName = "Lists/Rarity List")]
public class RarityList : ListHolder<EItemRarity>
{
    [SerializeField] private List<RarityColorPair> rarityColorPairs = new List<RarityColorPair>();

    private void OnEnable()
    {
        Initialize(ConvertToKeyValuePairs(rarityColorPairs));
    }

    private List<KeyValuePair<EItemRarity, Color>> ConvertToKeyValuePairs(List<RarityColorPair> pairs)
    {
        var keyValuePairs = new List<KeyValuePair<EItemRarity, Color>>();
        foreach (var pair in pairs)
        {
            keyValuePairs.Add(new KeyValuePair<EItemRarity, Color>(pair.rarity, pair.color));
        }
        return keyValuePairs;
    }
}
