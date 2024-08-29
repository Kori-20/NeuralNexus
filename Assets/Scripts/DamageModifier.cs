using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : MonoBehaviour
{
    public static readonly Dictionary<(EPhysicalDamageType, EDefenseStats), float> PhysicalDamageMultiplier = new Dictionary<(EPhysicalDamageType, EDefenseStats), float>()
    {
        
    };

    public static readonly Dictionary<(EElementalDamageType, EElementalDamageType), float> ElementalDamageMultiplier = new Dictionary<(EElementalDamageType, EElementalDamageType), float>()
    {
        {(EElementalDamageType.Crystal, EElementalDamageType.Crystal), 1.0f},
    };
}
