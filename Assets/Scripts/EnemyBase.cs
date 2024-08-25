using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    private int health = 100;
    private int damage = 10;
    private int armor = 0;
    private int shield = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void DamageCalc()
    {

    }

    void IDamageable.TakeDamage(int damage, EDamageType damageType)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage");
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }
    }
}
