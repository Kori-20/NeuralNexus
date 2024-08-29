using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 500;
    [SerializeField] private int damage = 10;
    [SerializeField] private int armor = 120;
    [SerializeField] private int shield = 200;

    [SerializeField] private EElementalDamageType myEnemyElement;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void IDamageable.CalculateDamage(int damage, EPhysicalDamageType physicalType, EElementalDamageType elementType)
    {
        int calcDmg = damage;
        calcDmg = (int)ElementMath(calcDmg, elementType);

        if (shield > 0)
        {
            ShieldMath(calcDmg, physicalType); //Deals damage to shield
        }
        else 
        {
            
            if (health > 0)
            {
                HealthMath(calcDmg, physicalType); //Deals damage to health or armored health
            }

            if (armor > 0)
            {
                ArmorMath(calcDmg, physicalType); //Deals damage to armor
            }
        }
    }

    private float ElementMath(int damage, EElementalDamageType gunElemType)
    {
        if (DamageModifier.ElementalDamageMultiplier.TryGetValue((gunElemType, myEnemyElement), out float multiplier))
        {
            return damage * multiplier;
        }
        return damage;
    }

    private void ShieldMath(int damage, EPhysicalDamageType physicalType) 
    {
        if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.Shield), out float multiplier)) { }
        else multiplier = 1.0f;

        shield -= (int)(damage * multiplier);
        if (shield < 0) shield = 0;
    }

    private void ArmorMath(int damage, EPhysicalDamageType physicalType)
    {
        if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.Armor), out float multiplier)) { }
        else multiplier = 1.0f;

        armor -= (int)(damage * multiplier);
        if (armor < 0) armor = 0;
    }

    private void HealthMath(int damage, EPhysicalDamageType physicalType)
    {
        if (armor > 0)
        {
            if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.ArmoredHealth), out float multiplier)) { }
            else multiplier = 1.0f;
            health -= (int)(damage * multiplier);
        }
        else
        {
            if (DamageModifier.PhysicalDamageMultiplier.TryGetValue((physicalType, EDefenseStats.Health), out float multiplier)) { }
            else multiplier = 1.0f;
            health -= (int)(damage * multiplier);
        }
        
        if (health <= 0)
        { 
            health = 0;
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
