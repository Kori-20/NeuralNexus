using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : MonoBehaviour
{
    public static readonly Dictionary<(EPhysicalDamageType, EDefenseStats), float> PhysicalDamageMultiplier = new Dictionary<(EPhysicalDamageType, EDefenseStats), float>()
    {
        {(EPhysicalDamageType.Blunt,EDefenseStats.Shield), 0.5f},
        {(EPhysicalDamageType.Blunt,EDefenseStats.Armor), 0.1f},
        {(EPhysicalDamageType.Blunt,EDefenseStats.Health), 1.2f},
        {(EPhysicalDamageType.Blunt,EDefenseStats.ArmoredHealth), 0.5f},

        {(EPhysicalDamageType.Piercing,EDefenseStats.Shield), 0.5f},
        {(EPhysicalDamageType.Piercing,EDefenseStats.Armor), 1.5f},

        {(EPhysicalDamageType.Explosive,EDefenseStats.Shield), 0.75f},
        {(EPhysicalDamageType.Explosive,EDefenseStats.ArmoredHealth), 0.75f},

        {(EPhysicalDamageType.Energetic,EDefenseStats.Shield), 1.5f},
        {(EPhysicalDamageType.Energetic,EDefenseStats.Armor), 0.1f},
        {(EPhysicalDamageType.Energetic,EDefenseStats.ArmoredHealth), .5f},
    };

    public static readonly Dictionary<(EElementalDamageType, EElementalDamageType), float> ElementalDamageMultiplier = new Dictionary<(EElementalDamageType, EElementalDamageType), float>()
    {
        {(EElementalDamageType.Void,EElementalDamageType.Crystal),1.2f },
        {(EElementalDamageType.Void,EElementalDamageType.Radiation),1.2f },
        {(EElementalDamageType.Void,EElementalDamageType.Magma),1.2f },
        {(EElementalDamageType.Void,EElementalDamageType.Plasma),1.2f },
        {(EElementalDamageType.Void,EElementalDamageType.Sound),1.2f },
        {(EElementalDamageType.Void,EElementalDamageType.Void),1.2f },

        {(EElementalDamageType.Sound,EElementalDamageType.Crystal),1.5f },
        {(EElementalDamageType.Sound,EElementalDamageType.Void),0.8f },

        {(EElementalDamageType.Crystal,EElementalDamageType.Magma),1.5f },
        {(EElementalDamageType.Crystal,EElementalDamageType.Radiation),0.8f },

        {(EElementalDamageType.Magma,EElementalDamageType.Plasma),1.5f },
        {(EElementalDamageType.Magma,EElementalDamageType.Void),0.8f },

        {(EElementalDamageType.Plasma,EElementalDamageType.Radiation),1.5f },
        {(EElementalDamageType.Plasma,EElementalDamageType.Void),0.8f },

        {(EElementalDamageType.Radiation,EElementalDamageType.Sound),1.5f },
        {(EElementalDamageType.Radiation,EElementalDamageType.Void),0.8f },
    };
}
