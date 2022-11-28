using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    ModManager modManager;
    ProjectileManager projManager;
    PlayerStatHandler stats;

    GameObject barrel;
    Vector3 barrelPos;
    public Vector3 SplitSpawnPoint { get; private set; }

    ProjectileSplitMod splitMod;
    ExplosiveProjectileMod explosiveMod;

    private void OnEnable()
    {
        barrelPos = barrel.transform.position;
    }
    private void Awake()
    {
        modManager = FindObjectOfType<ModManager>();
        projManager = FindObjectOfType<ProjectileManager>();
        stats = FindObjectOfType<PlayerStatHandler>();

        barrel = projManager.barrel;        

        splitMod = GetComponent<ProjectileSplitMod>();
        explosiveMod = GetComponent<ExplosiveProjectileMod>();

        splitMod.splitBullet1.SetActive(false);
        splitMod.splitBullet2.SetActive(false);
    }
    void Update()
    {
        LimitRange();
    }
    void LimitRange()
    {        
        float distance = Vector2.Distance(barrelPos, transform.position);
        if (distance >= stats.projectileRange)
        {
            if (modManager.EnableExplosivMod) explosiveMod.ShowExplosion(transform.position);

            if (modManager.EnableSplitMod)
            {
                splitMod.splitBullet1.transform.parent = null;
                splitMod.splitBullet2.transform.parent = null;

                splitMod.SplitProjectile(transform.position, transform.localRotation);

                SplitSpawnPoint = transform.position;
            }
            gameObject.SetActive(false);
            projManager.AddProjectileToPool(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable;
        if (collision.collider.tag != "Player")
        {            
            if (collision.gameObject.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.TakeDamage(stats.ReturnDamage());
            }

            if (modManager.EnableExplosivMod) explosiveMod.ShowExplosion(collision.contacts[0].point);

            if (modManager.EnableSplitMod)
            {
                splitMod.splitBullet1.transform.parent = null;
                splitMod.splitBullet2.transform.parent = null;

                splitMod.splitBullet1.SetActive(false);
                splitMod.splitBullet2.SetActive(false);

                splitMod.SplitProjectile(collision.contacts[0].point, transform.localRotation);

                SplitSpawnPoint = transform.position;
            }
            gameObject.SetActive(false);
            projManager.AddProjectileToPool(gameObject);
        }
    }

    //kun penetraatio p‰‰ll‰
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        if (collision.gameObject.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(stats.ReturnDamage());
        }
    }
}
