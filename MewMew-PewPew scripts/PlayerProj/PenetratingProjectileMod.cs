using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingProjectileMod : MonoBehaviour
{    
    Collider2D projectileCollider;
    ModManager modManager;
    private void Awake()
    {
        modManager = FindObjectOfType<ModManager>();
        projectileCollider = GetComponent<Collider2D>();        
    }
    private void OnEnable()
    {
        ProjectileBehaviour();
    }
    public void ProjectileBehaviour()
    {
        if (modManager.EnablePenetrateMod) projectileCollider.isTrigger = true;
        else projectileCollider.isTrigger = false;
    }    
}
