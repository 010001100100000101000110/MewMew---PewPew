using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    PlayerStatHandler stats;

    float timer;
    void Awake()
    {
        stats = FindObjectOfType<PlayerStatHandler>();
    }

    private void OnEnable()
    {
        timer = 0.2f;
    }
    private void Update()
    {
        if (gameObject.activeInHierarchy) timer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timer > 0)
        {
            IDamageable damageable;
            if (collision.gameObject.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.TakeDamage(stats.ReturnDamage());
            }
        }
        
    }
}
