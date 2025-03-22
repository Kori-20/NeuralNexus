using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDatabaseWindow : EditorWindow
{
    private Vector2 scrollPos;

    [MenuItem("Neural Nexus/Item Database Viewer")]
    public static void ShowWindow()
    {
        GetWindow<ItemDatabaseWindow>("Item Database");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Item Database", EditorStyles.boldLabel);

        if (CSVReader.itemDictionary == null || CSVReader.itemDictionary.Count == 0)
        {
            EditorGUILayout.HelpBox("No items loaded. Make sure to run CSVReader.LoadAllCSVData()!", MessageType.Warning);
            if (GUILayout.Button("Load Items"))
            {
                CSVReader.LoadAllCSVData();
            }
            return;
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (KeyValuePair<int, Item> entry in CSVReader.itemDictionary)
        {
            Item item = entry.Value;

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"ID: {item.ID} | Name: {item.Name}", EditorStyles.boldLabel);

            if (item is Gun gun)
            {
                EditorGUILayout.LabelField($"Type: {gun.WeaponType} | Damage: {gun.Damage} | Fire Rate: {gun.FireRate}");
            }
            else if (item is Armor armor)
            {
                EditorGUILayout.LabelField($"Type: Armor | Defense: {armor.Defense}");
            }
            else if (item is Consumable consumable)
            {
                EditorGUILayout.LabelField($"Type: Consumable | Stack Size: {consumable.StackSize}");
            }
            else
            {
                EditorGUILayout.LabelField("Type: Unknown");
            }

            if (item.Icon != null)
            {
                GUILayout.Label(item.Icon.texture, GUILayout.Width(50), GUILayout.Height(50));
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();
    }
}
