using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Blue, Red, Green}
public class EnemyProjectileManager : MonoBehaviour
{

    [SerializeField] List<GameObject> BlueProjectiles = new List<GameObject>();
    [SerializeField] List<GameObject> RedProjectiles = new List<GameObject>();
    [SerializeField] List<GameObject> GreenProjectiles = new List<GameObject>();

    [SerializeField] int enemyProjectileListLength; 

    [SerializeField] List<GameObject> enemyProjectilePrefabs = new List<GameObject>();
    void OnEnable()
    {
        InstantiateEnemyProjectiles(0);
        InstantiateEnemyProjectiles(1);
        InstantiateEnemyProjectiles(2);
    }

    void InstantiateEnemyProjectiles(int type)
    {
        for (int i = 0; i < enemyProjectileListLength; i++)
        {
            GameObject projectile = Instantiate(enemyProjectilePrefabs[type], transform.position, Quaternion.identity);           

            projectile.SetActive(false);
            projectile.transform.parent = this.transform;
            if (type == 0) BlueProjectiles.Add(projectile);
            if (type == 1) RedProjectiles.Add(projectile);
            if (type == 2) GreenProjectiles.Add(projectile);
        }
    }

    public GameObject GetEnemyProjectile(ProjectileType type, Vector3 enemyBarrelPos)
    {
        List<GameObject> enemyProjectiles = new List<GameObject>();
        GameObject projectile; 
        if (type == ProjectileType.Red) enemyProjectiles = RedProjectiles;
        if (type == ProjectileType.Blue) enemyProjectiles = BlueProjectiles;
        if (type == ProjectileType.Green) enemyProjectiles = GreenProjectiles;

        if (enemyProjectiles.Count > 0)
        {
            for (int i = 0; i < enemyProjectiles.Count; i++)
            {                
                projectile = enemyProjectiles[i];
                return projectile;
            }
            return null;
        }
        else
        {
            int projectileType = ((int)type);
            projectile = Instantiate(enemyProjectilePrefabs[projectileType], enemyBarrelPos, Quaternion.identity);
            return projectile;
        }
    }

    public void AddEnemyProjectileToPool(GameObject projectile, ProjectileType type)
    {
        if (type == ProjectileType.Red) RedProjectiles.Add(projectile);
        if (type == ProjectileType.Blue) BlueProjectiles.Add(projectile);
        if (type == ProjectileType.Green) GreenProjectiles.Add(projectile);
    }

    public void RemoveEnemyProjectileFromPool(GameObject projectile, ProjectileType type)
    {
        if (type == ProjectileType.Red) RedProjectiles.Remove(projectile);
        if (type == ProjectileType.Blue) BlueProjectiles.Remove(projectile);
        if (type == ProjectileType.Green) GreenProjectiles.Remove(projectile);
    }

}
