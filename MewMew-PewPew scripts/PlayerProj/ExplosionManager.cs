using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public List<GameObject> Explosions = new List<GameObject>();
    int projectileListLength;

    [SerializeField] GameObject explosionPrefab;    
    //GameObject explosion;

    ProjectileManager projectileManager;

    void OnEnable()
    {
        projectileManager = GetComponent<ProjectileManager>();
        projectileListLength = projectileManager.projectileListLength;
        InstantiateExplosions();
    }

    void InstantiateExplosions()
    {
        for (int i = 0; i < (projectileListLength * 3); i++)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.SetActive(false);
            explosion.transform.parent = this.transform;
            Explosions.Add(explosion);
        }
    }

    public GameObject GetExplosion(Vector2 explosionPoint)
    {
        GameObject explosion;
        if (Explosions.Count > 0)
        {
            for (int i = 0; i < Explosions.Count; i++)
            {
                if (Explosions[i].activeInHierarchy == false)
                {
                    Explosions[i].SetActive(true);
                    explosion = Explosions[i];
                    return explosion;
                }
            }
            return null;
        }
        else
        {
            explosion = Instantiate(explosionPrefab, explosionPoint, Quaternion.identity);
            explosion.transform.parent = this.transform;
            return explosion;
        }
    }
    public void AddExplosionToPool(GameObject explosion)
    {
        Explosions.Add(explosion);
    }
}
