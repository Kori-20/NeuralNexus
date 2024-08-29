using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
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

    void IDamageable.TakeDamage(int damage,EPhysicalDamageType physicalType, EElementalDamageType elementType)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage");
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }
    }

    private void CalculateDamage()
    {

    }
}
