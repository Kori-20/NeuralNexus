using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListHolder<TKey> : ScriptableObject where TKey : Enum
{
    protected Dictionary<TKey, Color> keyColorMap = new Dictionary<TKey, Color>();

    public virtual void Initialize(List<KeyValuePair<TKey, Color>> pairs)
    {
        keyColorMap.Clear();
        foreach (var pair in pairs)
        {
            if (!keyColorMap.ContainsKey(pair.Key))
            {
                keyColorMap[pair.Key] = pair.Value;
            }
        }
    }

    public Color GetElementColor(TKey key)
    {
        if (keyColorMap == null) return Color.white; // Fallback color
        return keyColorMap.TryGetValue(key, out var color) ? color : Color.white;
    }
}
