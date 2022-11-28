using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAuraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] List<GameObject> auraProjectiles = new List<GameObject>();
    float projectileListLength = 40;

    [SerializeField] Powerup_DamageAura sODamageAura;

    GameObject player;

    [SerializeField] float slerpTime;

    Vector3 refVel;

    bool damageAuraActive;
    bool buffedDamageAuraActive;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        InstantiateProjectiles();
    }

    void InstantiateProjectiles()
    {
        for (int i = 0; i < projectileListLength; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.SetActive(false);
            projectile.transform.parent = this.transform;
            auraProjectiles.Add(projectile);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G) && !damageAuraActive)
        //{
        //    SpawnProjectiles();
        //}

        //if (Input.GetKeyDown(KeyCode.H) && damageAuraActive)
        //{
        //    DespawnProjectiles();
        //}

        //MUISTA POISTAA NOI KOMMENTISTA vvvvvvvvvvvvvvvvvvvvvv             

        if (sODamageAura.PowerUpActive && !damageAuraActive)
        {
            damageAuraActive = true;
            SpawnProjectiles();
        }
                
        if (!sODamageAura.PowerUpActive && damageAuraActive)
        {
            damageAuraActive = false;
            buffedDamageAuraActive = false;
            DespawnProjectiles();
        }
    }
    void LateUpdate()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref refVel, slerpTime);
        transform.position = tempPos;
        if (damageAuraActive) RotateBullets();
    }

    public void SpawnProjectiles()
    {        
        Vector3 center = transform.position;
        for (int i = 0; i < sODamageAura.ProjectileAmount; i++)
        {
            float angle = 360 / sODamageAura.ProjectileAmount * i;
            Vector3 pos = PointAroundPlayer(center, sODamageAura.Radius, angle);
            GameObject bullet = GetAuraProjectile(pos);           
            if (bullet.transform.parent == null) bullet.transform.parent = transform;

        }
    }

    public void DespawnProjectiles()
    {        
        for (int i = 0; i < auraProjectiles.Count; i++)
        {
            auraProjectiles[i].SetActive(false);
        }
    }

    //projectile positioning
    Vector3 PointAroundPlayer(Vector3 playerPos, float radius, float angle)
    {
        Vector3 pos;
        pos.x = playerPos.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = playerPos.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = playerPos.z;
        return pos;
    } 
    public GameObject GetAuraProjectile(Vector3 pos)
    {
        GameObject projectile;
        if (auraProjectiles.Count > 0)
        {
            for (int i = 0; i < auraProjectiles.Count; i++)
            {
                if (auraProjectiles[i].activeInHierarchy == false)
                {
                    auraProjectiles[i].SetActive(true);
                    projectile = auraProjectiles[i];
                    projectile.transform.position = pos;
                    return projectile;
                }
            }
            return null;
        }
        else
        {
            projectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
            return projectile;
        }
    }
  
    void RotateBullets()
    {
        transform.Rotate(0, 0, sODamageAura.RotationSpeed * Time.deltaTime, Space.Self);
    }
}
