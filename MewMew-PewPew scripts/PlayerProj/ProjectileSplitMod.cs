using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSplitMod : MonoBehaviour
{
    PlayerStatHandler stats;
    ModManager modManager;
    GameObject barrel;
    Vector3 barrelPos;

    [SerializeField] public GameObject splitBullet1;
    [SerializeField] public GameObject splitBullet2;

    private void Awake()
    {
        barrel = GameObject.FindWithTag("Barrel");
        stats = FindObjectOfType<PlayerStatHandler>();
        modManager = FindObjectOfType<ModManager>();
    }

    private void OnEnable()
    {
        barrelPos = barrel.transform.position;
    }
    public void SplitProjectile(Vector2 splittingPoint, Quaternion mainProjRot)
    {
        if (modManager.EnableSplitMod)
        {
            Vector2 barrelPosV2 = barrelPos;

            Vector2 dir = barrelPosV2 - splittingPoint;
            dir.Normalize();
            Vector2 splitDir = Vector2.Perpendicular(dir);

            GameObject bullet = splitBullet1;
            GameObject bullet2 = splitBullet2;

            bullet.transform.position = splittingPoint;
            bullet2.transform.position = splittingPoint;

            //rotating the bullet vvvvvv
            bullet.transform.rotation = Quaternion.Euler(0, 0, mainProjRot.eulerAngles.z - 90);
            bullet2.transform.rotation = Quaternion.Euler(0, 0, mainProjRot.eulerAngles.z + 90);

            bullet.SetActive(true);
            bullet2.SetActive(true);

            bullet.GetComponent<Rigidbody2D>().AddForce((splitDir) * stats.projectileSpeed, ForceMode2D.Impulse);
            bullet2.GetComponent<Rigidbody2D>().AddForce((-splitDir) * stats.projectileSpeed, ForceMode2D.Impulse);
        }        
    }    
}
