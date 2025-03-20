using UnityEngine;
using UnityEditor;

public class ItemEditor : EditorWindow
{

    private int customID = 0;

    [MenuItem("Tools/CSV Reader")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditor>("CSV Reader");
    }

    private void OnGUI()
    {
        GUILayout.Label("CSV Reader", EditorStyles.boldLabel);

        

        if (GUILayout.Button("Load All CSV Data"))
        {
            CSVReader.LoadAllCSVData();
            Debug.Log("CSV Data Loaded");
        }

        customID = EditorGUILayout.IntField("ID:", customID);

        if (GUILayout.Button("Test Dictionary"))
        {
            Item item = CSVReader.GetItem(customID);
            if (item is Gun)
            {
                Gun gun = item as Gun;
                Debug.Log("Name: " + gun.Name + " ID: " + gun.ID + " Desc: " + gun.Description + " FireRate: " + gun.FireRate);
            }
        }
    }
}
