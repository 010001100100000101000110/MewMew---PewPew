using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageAura", menuName = "ScriptableObjects/Powerup/DamageAura")]
public class Powerup_DamageAura : PowerupBaseSO
{
    public float Radius;
    [HideInInspector] public float RotationSpeed { get; private set; }
    public float BuffedRotationSpeed;
    public float NormalRotationSpeed;
    [field: SerializeField]public int ProjectileAmount{ get; private set; }
    public int BuffedProjectileAmount;

    public bool PowerUpActive { get; private set; }
    public bool BuffedPowerUpActive { get; private set; }

    public override void EndPowerup()
    {
        PowerUpActive = false;
    }

    protected override void BuffedPowerup()
    {
        PowerUpActive = true;
        ProjectileAmount = BuffedProjectileAmount;
        RotationSpeed = BuffedRotationSpeed;
    }

    protected override void NormalPowerup()
    {
        PowerUpActive = true;
        ProjectileAmount = statHandler.projectileAmount;
        RotationSpeed = NormalRotationSpeed;
    }
}
