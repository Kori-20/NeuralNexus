using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/DefaultItem")]
public class Item : ScriptableObject
{
    [Header("Item Properties")]
    [SerializeField] protected string id;
    [SerializeField] new protected string name;
    [SerializeField] public Sprite sprite;
    [SerializeField, TextArea(2, 10)] protected string description;
    [SerializeField] public EItemRarity rarity;
    public Color rarityColor;

    [SerializeField] protected int stackSize;

    [SerializeField] protected bool isStackable;
    [SerializeField] protected bool isEquippable;
    [SerializeField] protected bool isDiscardable;
    [SerializeField] protected bool isConsumable;

    private void OnValidate()
    {
        if(FindAnyObjectByType<GameConfigManager>()) rarityColor = FindAnyObjectByType<GameConfigManager>().GetRarityColor(rarity);
    }
}
