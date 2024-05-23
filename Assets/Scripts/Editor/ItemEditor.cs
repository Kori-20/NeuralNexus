using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    private EItemRarity previousRarity;

    public override void OnInspectorGUI()
    {
        Item item = (Item)target;

        if (GUILayout.Button("Load Color")) //Manual Activation
        {
            item.rarityColor = FindAnyObjectByType<GameConfigManager>().GetRarityColor(item.rarity);
        }

        base.OnInspectorGUI();

        if (item.rarity != previousRarity)
        {
            item.rarityColor = FindAnyObjectByType<GameConfigManager>().GetRarityColor(item.rarity);
            previousRarity = item.rarity;
            EditorUtility.SetDirty(item);
        }
    }
}
