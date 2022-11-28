using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<GameObject> Projectiles = new List<GameObject>();
    [field: SerializeField] public int projectileListLength { get; private set; }

    [SerializeField] GameObject projectilePrefab;
    public GameObject barrel { get; private set; }

    PlayerStatHandler stats;
    void OnEnable()
    {
        stats = FindObjectOfType<PlayerStatHandler>();
        barrel = GameObject.FindWithTag("Barrel");
        InstantiateProjectiles();
    }

    void InstantiateProjectiles()
    {
        for (int i = 0; i < projectileListLength; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.SetActive(false);
            projectile.transform.parent = this.transform;
            Projectiles.Add(projectile);
        }
    }    
    public GameObject GetProjectile()
    {
        GameObject bullet;
        if (Projectiles.Count > 0)
        {
            return Projectiles[0];
        }
        else
        {
            bullet = Instantiate(projectilePrefab, barrel.transform.position, Quaternion.identity);
            bullet.transform.parent = this.transform;
            return bullet;
        }
    }
    public void AddProjectileToPool(GameObject projectile)
    {
        Projectiles.Add(projectile);
    }
    public void SetProjectileMaxDistance(float maxDis)
    {
        maxDis = stats.projectileRange;
    }
    public void SetProjectileDamage(float damage)
    {
        damage = stats.damage;
    }
}
