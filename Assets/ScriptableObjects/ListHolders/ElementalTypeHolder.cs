using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TypeColorPair
{
    public EElementalDamageType type;
    public Color color;
}

[CreateAssetMenu(fileName = "NewElementalTypeList", menuName = "Lists/Elemental Type List")]
public class ElementalTypeList : ListHolder<EElementalDamageType>
{
    [SerializeField] private List<TypeColorPair> typeColorPairs = new List<TypeColorPair>();

    private void OnEnable()
    {
        Initialize(ConvertToKeyValuePairs(typeColorPairs));
    }

    private List<KeyValuePair<EElementalDamageType, Color>> ConvertToKeyValuePairs(List<TypeColorPair> pairs)
    {
        var keyValuePairs = new List<KeyValuePair<EElementalDamageType, Color>>();
        foreach (var pair in pairs)
        {
            keyValuePairs.Add(new KeyValuePair<EElementalDamageType, Color>(pair.type, pair.color));
        }
        return keyValuePairs;
    }
}
