using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable, IDamageableHit
{
    [SerializeField] private int health = 500;
    [SerializeField] private int damage = 10;
    [SerializeField] private int armor = 120;
    [SerializeField] private int shield = 200;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void DamageCalc()
    {

    }

    void IDamageableHit.CalculateDamage()
    {
        TakeDamage(0, 0, 0);
    }

    void IDamageable.CalculateDamage(int damage,EPhysicalDamageType physicalType, EElementalDamageType elementType)
    {
        TakeDamage(10,20,20);
    }

    private void TakeDamage(int shieldDamage, int armorDamage, int healthDamage)
    {
        if (shieldDamage > 0) Debug.Log("Enemy Shield took " + shieldDamage + " damage");
        if (armorDamage > 0) Debug.Log("Enemy Armor took " + armorDamage + " damage");
        if (healthDamage > 0) Debug.Log("Enemy Health took " + healthDamage + " damage");

        int totalDamage = shieldDamage + armorDamage + healthDamage;
        if (totalDamage > 0) Debug.Log("Enemy took a total of" + totalDamage + " damage");
        else Debug.Log("Enemy took no damage");

        /*
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage");
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }
        */
    }

    private void ShieldMath(int damage, EPhysicalDamageType physicalType) 
    { 

    }

    private void ArmorMath(int damage, EElementalDamageType elementType)
    {

    }

    private void HealthMath(int damage, EPhysicalDamageType physicalType, EElementalDamageType elementType)
    {

    }

    private void ElementMath(int damage, EElementalDamageType elementType)
    {

    }
}
