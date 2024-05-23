using UnityEngine;

public class Ability : ScriptableObject 
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected string abilityDescription;
    [SerializeField] protected Sprite abilityIcon;
    [SerializeField] protected float cooldown;

    //Some abilites can subscribe to specific events such as OnKill, OnDamage, OnHeal, etc.

    protected virtual void CastAbility()
    {
        //This method will be overriden by the child classes
    }
}
