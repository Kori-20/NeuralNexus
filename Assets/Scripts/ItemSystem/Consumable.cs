using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Consumable")]
public class Consumable : Item
{
    protected int numberOfUses;

    protected virtual void Consume()
    {
         
    }
}
