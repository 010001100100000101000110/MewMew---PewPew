using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectileMod : MonoBehaviour
{    
    ModManager modManager;
    ExplosionManager explosionManager;
    private void Awake()
    {
        modManager = FindObjectOfType<ModManager>();
        explosionManager = FindObjectOfType<ExplosionManager>();
    }

    public void ShowExplosion(Vector2 explosionPoint)
    {
        if (modManager.EnableExplosivMod)
        {
            GameObject explosion = explosionManager.GetExplosion(explosionPoint);
            explosion.transform.position = explosionPoint;
            explosionManager.Explosions.Remove(explosion);
        }        
    }
}
