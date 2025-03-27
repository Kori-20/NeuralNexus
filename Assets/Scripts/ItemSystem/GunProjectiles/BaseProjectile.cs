using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [Header("Projectile Stats")]
    [SerializeField] int damage = 1;
    [SerializeField] int speed = 10;

    [Header("Projectile Type")]
    [SerializeField] EPhysicalDamageType physical;
    [SerializeField] EElementalDamageType element;

    [Header("Projectile References")]
    [SerializeField] Vector3 moveDir;
    [SerializeField] SphereCollider hitBox;


    //Call Init as bullet is instantiated
    public void InitProjectile(int bulletDamage, int bulletVelocity, EPhysicalDamageType physicalType, EElementalDamageType elementalType, Vector3 bulletDir)
    {
        hitBox = GetComponent<SphereCollider>(); //Check for errors
        damage = bulletDamage;
        speed = bulletVelocity;
        physical = physicalType;
        element = elementalType;
        moveDir = bulletDir;
    }

    private void Update()
    {
        // Move the bullet along its forward axis
        //transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Projectile" + name + "Collided with" + other.name);

        if (other.gameObject.TryGetComponent(out IDamageableHit damageableHit))
        {
            damageableHit.CalculateDamage();
        }
        else if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.CalculateDamage(damage, physical, element);
        }
        else if (other.gameObject.TryGetComponent(out IDamageableElement damageableElement))
        {
            damageableElement.CalculateDamage(physical);
        }
        else if (other.gameObject.TryGetComponent(out IDamageableDamage damageableDamage))
        {
            damageableDamage.CalculateDamage(damage);
        }
        else
        {
            Debug.Log("No Damageable Interface Found");
        }

        ProjectileManager.Instance.RemoveFromPool(gameObject);
    }
}
