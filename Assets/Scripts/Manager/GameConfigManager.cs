using UnityEngine;
public class GameConfigManager : MonoBehaviour
{
    private static GameConfigManager thisInstance;
    public static GameConfigManager Instance => thisInstance;

    [SerializeField] private RarityList rarityList;
    [SerializeField] private ElementalTypeList elementalTypeHolder;

    private void Awake()
    {
        if (thisInstance == null)
        {
            thisInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public Color GetRarityColor(EItemRarity rarity)
    {
        return rarityList.GetElementColor(rarity);
    }

    public Color GetElementalColor(EElementalDamageType elementType)
    {
        return elementalTypeHolder.GetElementColor(elementType);
    }
}
