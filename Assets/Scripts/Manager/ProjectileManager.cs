using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager thisInstance;
    public static ProjectileManager Instance => thisInstance;

    private List<GameObject> enemyProjectileList = new List<GameObject>();
    private List<GameObject> playerProjectileList = new List<GameObject>();

    private Transform playerProjectileParent;
    private Transform enemyProjectileParent;

    private void Awake()
    {
        if (thisInstance == null)
        {
            thisInstance = this;
            InitializeProjectileParents();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeProjectileParents()
    {
        // Create or find Player Projectiles parent
        playerProjectileParent = new GameObject("PlayerProjectiles").transform;
        playerProjectileParent.SetParent(transform);

        // Create or find Enemy Projectiles parent
        enemyProjectileParent = new GameObject("EnemyProjectiles").transform;
        enemyProjectileParent.SetParent(transform);
    }

    public void SpawnProjectile(GameObject projectilePrefab, Vector3 shootPoint, Vector3 direction, int damage, int velocity, EPhysicalDamageType physicalType, EElementalDamageType elementType, EProjectileType type)
    {
        GameObject activeBullet = Instantiate(projectilePrefab, shootPoint, Quaternion.identity);
        activeBullet.transform.forward = direction.normalized;
        activeBullet.GetComponent<BaseProjectile>().InitProjectile(damage, velocity, physicalType, elementType, activeBullet.transform.forward);
        AddToProjectilePool(activeBullet, type);
    }

    private void AddToProjectilePool(GameObject projectileToAdd, EProjectileType type)
    {
        switch (type)
        {
            case EProjectileType.Player:
                playerProjectileList.Add(projectileToAdd);
                projectileToAdd.transform.SetParent(playerProjectileParent);
                break;

            case EProjectileType.Enemy:
                enemyProjectileList.Add(projectileToAdd);
                projectileToAdd.transform.SetParent(enemyProjectileParent);
                break;
        }
    }

    public void RemoveFromPool(GameObject projectile)
    {
        if (playerProjectileList.Contains(projectile)) playerProjectileList.Remove(projectile);
        else if (enemyProjectileList.Contains(projectile)) enemyProjectileList.Remove(projectile);
        else
        {
            Debug.LogWarning("Projectile not found in pool");
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
