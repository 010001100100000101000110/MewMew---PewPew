using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraProjectileBehaviour : MonoBehaviour
{
    PlayerStatHandler stats;
    
    void Awake()
    {
        stats = FindObjectOfType<PlayerStatHandler>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable;
        if (collision.gameObject.tag != "Projectile")
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.TakeDamage(stats.ReturnDamage());
            }           
        }
    }
}
