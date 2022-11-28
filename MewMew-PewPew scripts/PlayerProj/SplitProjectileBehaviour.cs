using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitProjectileBehaviour : MonoBehaviour
{

    PlayerStatHandler stats;
    ModManager modManager;
    ExplosiveProjectileMod explosiveMod;
    ProjectileBehaviour projBehaviour;
    [SerializeField] GameObject parent;



    private void Awake()
    {
        stats = FindObjectOfType<PlayerStatHandler>();
        modManager = FindObjectOfType<ModManager>();
        explosiveMod = GetComponent<ExplosiveProjectileMod>();   
        projBehaviour = parent.GetComponent<ProjectileBehaviour>();
    }
    void Update()
    {
        LimitSplitRange();
    }

    void LimitSplitRange()
    {
        float distance = Vector2.Distance(projBehaviour.SplitSpawnPoint, transform.position);
        
        if (distance >= stats.projectileRange)
        {
            if (modManager.EnableExplosivMod) explosiveMod.ShowExplosion(transform.position);
            gameObject.SetActive(false);
            this.transform.parent = parent.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        IDamageable damageable;
        if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Player")
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.TakeDamage(stats.ReturnDamage());
            }            
        }               
    }
}
