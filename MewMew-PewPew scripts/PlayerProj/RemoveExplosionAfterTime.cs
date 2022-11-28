using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveExplosionAfterTime : MonoBehaviour
{
    [SerializeField] float explosionDuration;
    ExplosionManager explosionManager;

    private void OnEnable()
    {
        StartCoroutine(AddExplosionToPoolAfterSeconds(explosionDuration));
    }
    void Awake()
    {
        explosionManager = FindObjectOfType<ExplosionManager>();
    }

    IEnumerator AddExplosionToPoolAfterSeconds(float duration)
    {       
        yield return new WaitForSeconds(duration);
        explosionManager.AddExplosionToPool(gameObject);
        gameObject.SetActive(false);
    }
}
