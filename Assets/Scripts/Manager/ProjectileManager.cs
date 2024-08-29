using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager thisInstance;
    public static ProjectileManager Instance => thisInstance;

    private List<GameObject> enemyProjectileList = new List<GameObject>();
    private List<GameObject> playerProjectileList = new List<GameObject>();

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

    public void SpawnProjectile(GameObject projectilePrefab, Vector3 shootPoint, Vector3 direction, int damage, int velocity, EPhysicalDamageType physicalType, EElementalDamageType elementType)
    {
        GameObject activeBullet = Instantiate(projectilePrefab, shootPoint, Quaternion.LookRotation(direction), transform);
        activeBullet.GetComponent<BaseProjectile>().InitProjectile(damage, velocity, physicalType, elementType, direction);
        AddToProjetilePool(activeBullet, EProjectileType.Player);
    }

    private void AddToProjetilePool(GameObject projectileToAdd, EProjectileType type)
    {
        switch(type)
        {
            case EProjectileType.Player:
                playerProjectileList.Add(projectileToAdd);
                break;

            case EProjectileType.Enemy:
                enemyProjectileList.Add(projectileToAdd);
                break;
        }
    }

    public void RemoveFromPool(GameObject projectile)
    {
        if (playerProjectileList.Contains(projectile)) playerProjectileList.Remove(projectile);
        else if (enemyProjectileList.Contains(projectile)) enemyProjectileList.Remove(projectile);
        else 
        { 
            Debug.LogError("Projectile not found in pool"); 
        }
        Destroy(projectile);
    }
}

public enum EProjectileType
{
    Player,
    Enemy
}

public enum EProjectilePrefab
{
    smallCaliber,
    mediumCaliber,
    largeCaliber,
    rocket,
    grenade
}
