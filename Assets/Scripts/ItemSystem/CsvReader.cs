using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class CSVReader
{
    private static Dictionary<int, Item> itemDictionary = new Dictionary<int, Item>();

    public static void LoadAllCSVData()
    {
        LoadItems("Guns.csv", ParseGun);
        //LoadItems("Armor.csv", ParseArmor);
        //LoadItems("Consumables.csv", ParseConsumable);
        //LoadItems("QuestItems.csv", ParseQuestItem);
        //LoadItems("GeneralItems.csv", ParseGeneralItem);

        Debug.Log("All CSV Data Loaded Successfully.");
    }

    private static void LoadItems(string fileName, Func<string[], Item> parseFunction)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "DataTables", fileName);
        if (!File.Exists(filePath))
        {
            Debug.LogError($"Missing CSV File: {filePath}");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++) // Skip header
        {
            string[] data = lines[i].Trim().Split(',');
            if (data.Length == 0 || string.IsNullOrWhiteSpace(data[0])) continue; // Skip empty lines

            Item item = parseFunction(data);
            if (item != null)
            {
                itemDictionary[item.ID] = item;
            }
        }
    }


    private static Item ParseGun(string[] data)
    {
        return new Gun(
            id: int.Parse(data[0]),
            name: data[1],
            description: data[2],
            rarity: (EItemRarity)Enum.Parse(typeof(EItemRarity), data[3]),
            icon: LoadSprite(data[4]),
            uniqueEffect: data[5],
            nonAbbreviatedName: data[6],
            durability: float.Parse(data[7]),
            maxDurability: float.Parse(data[8]),
            damage: int.Parse(data[9]),
            fireRate: float.Parse(data[10]),
            range: int.Parse(data[11]),
            accuracy: int.Parse(data[12]),
            velocity: int.Parse(data[13]),
            recoil: float.Parse(data[14]),
            pelletsPerShot: int.Parse(data[15]),
            isAutomatic: bool.Parse(data[16]),
            reloadTime: float.Parse(data[17]),
            magazineSize: int.Parse(data[18]),
            magsStored: int.Parse(data[19]),
            weaponType: (EWeaponType)Enum.Parse(typeof(EWeaponType), data[20]),
            physicalType: (EPhysicalDamageType)Enum.Parse(typeof(EPhysicalDamageType), data[21]),
            elementType: (EElementalDamageType)Enum.Parse(typeof(EElementalDamageType), data[22])
        );
    }

    //private static Item ParseArmor(string[] data)
    //{
    //    return new Armor(
    //        id: int.Parse(data[0]),
    //        name: data[1],
    //        description: data[2],
    //        rarity: (EItemRarity)Enum.Parse(typeof(EItemRarity), data[3]),
    //        Icon: LoadSprite(data[4]),
    //        uniqueEffect: data[5],
    //        nonAbbreviatedName: data[6],
    //        durability: float.Parse(data[7]),
    //        maxDurability: float.Parse(data[8]),
    //        defense: int.Parse(data[9])
    //    );
    //}

    //private static Item ParseConsumable(string[] data)
    //{
    //    return new Consumable(
    //        id: int.Parse(data[0]),
    //        name: data[1],
    //        description: data[2],
    //        rarity: (EItemRarity)Enum.Parse(typeof(EItemRarity), data[3]),
    //        icon: LoadSprite(data[4]),
    //        stackSize: int.Parse(data[5]),
    //        isStackable: bool.Parse(data[6])
    //    );
    //}

    //private static Item ParseQuestItem(string[] data)
    //{
    //    return new QuestItem(
    //        id: int.Parse(data[0]),
    //        name: data[1],
    //        description: data[2],
    //        rarity: (EItemRarity)Enum.Parse(typeof(EItemRarity), data[3]),
    //        icon: LoadSprite(data[4])
    //    );
    //}

    //private static Item ParseGeneralItem(string[] data)
    //{
    //    return new GeneralItem(
    //        id: int.Parse(data[0]),
    //        name: data[1],
    //        description: data[2],
    //        rarity: (EItemRarity)Enum.Parse(typeof(EItemRarity), data[3]),
    //        icon: LoadSprite(data[4]),
    //        stackSize: int.Parse(data[5]),
    //        isStackable: bool.Parse(data[6])
    //    );
    //}

    private static Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public static Item GetItem(int id) => itemDictionary.TryGetValue(id, out var item) ? item : null;
}
