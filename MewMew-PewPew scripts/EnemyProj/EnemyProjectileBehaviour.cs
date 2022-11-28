using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBehaviour : MonoBehaviour
{
    [SerializeField]float time;
    [SerializeField] SOEnemyProjectileStats enemyProjectileStats;
    EnemyProjectileManager enemyProjectileManager;
    void Awake()
    {
        enemyProjectileManager = FindObjectOfType<EnemyProjectileManager>();
        time = 2.5f;
    }
    private void OnEnable()
    {
        StartCoroutine(AddProjectileToPoolAfterSeconds(time));
    }

    IEnumerator AddProjectileToPoolAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
        enemyProjectileManager.AddEnemyProjectileToPool(gameObject, enemyProjectileStats.projectileType);        
    }
}
