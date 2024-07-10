using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "STAttackStrategyUpgraded",
    menuName = "Scriptable Objects/Towers/STAttackStrategyUpgraded")]
/// <summary>
/// Concrete Scriptable object for the single target attacker
/// </summary>
public class STAttackStrategyUpgraded : AbstractAttackStrategy
{
    [SerializeField]
    protected int damage; //Damage of projectile
    [SerializeField]
    private ParticleSystem flareParticleSystemPrefab;
    [SerializeField]
    private float flarePrefabPositionCorrectionY;
    [SerializeField]
    private float flarePrefabPositionCorrectionXLeft;
    [SerializeField]
    private float flarePrefabPositionCorrectionXRight;
    [SerializeField]
    private float projectileYCorrection;

    private ParticleSystem flareParticleSystemInstanceLeft;
    private ParticleSystem flareParticleSystemInstanceRight;
    private Light2D weaponLightLeft;
    private Light2D weaponLightRight;
    private float weaponLightIntensity;

    /// <summary>
    /// Returns damage field
    /// </summary>
    public int GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// Called on start by tower controler
    /// </summary>
    public override void OnStart()
    {
        base.OnStart();

        flareParticleSystemInstanceLeft = InstantiateParitcleSystemPrefabs(flareParticleSystemPrefab, flarePrefabPositionCorrectionY, true);
        flareParticleSystemInstanceRight = InstantiateParitcleSystemPrefabs(flareParticleSystemPrefab, flarePrefabPositionCorrectionY, false);

        weaponLightLeft = weapon.gameObject.transform.GetChild(0).GetComponent<Light2D>();
        weaponLightLeft.transform.localPosition = new Vector3(flarePrefabPositionCorrectionXLeft, flarePrefabPositionCorrectionY, 0);
        weaponLightRight = weapon.gameObject.transform.GetChild(1).GetComponent<Light2D>();
        weaponLightRight.transform.localPosition = new Vector3(flarePrefabPositionCorrectionXRight, flarePrefabPositionCorrectionY, 0);
    }

    /// <summary>
    /// Stops the animtion on first frame
    /// </summary>
    public override void OnFirstFrame()
    {
        animator.speed = 0;

        if (flareParticleSystemInstanceLeft == null)
        {
            weaponLightLeft.intensity = weaponLightIntensity;
            flareParticleSystemInstanceLeft = InstantiateParitcleSystemPrefabs(flareParticleSystemPrefab, flarePrefabPositionCorrectionY, true);
        }
        if (flareParticleSystemInstanceRight == null)
        {
            weaponLightRight.intensity = weaponLightIntensity;
            flareParticleSystemInstanceRight = InstantiateParitcleSystemPrefabs(flareParticleSystemPrefab, flarePrefabPositionCorrectionY, false);
        }
    }

    /// <summary>
    /// Concrete implemetation of CreateProjectile function. Creates a single target projectile and sets correct rotation and position
    /// </summary>
    protected override void CreateProjectile(List<EnemyController> pTargets, Quaternion pRotation, float pSpeed)
    {
        if (flareParticleSystemInstanceLeft != null)
        {
            if (weaponLightIntensity < weaponLightLeft.intensity)
            {
                weaponLightIntensity = weaponLightLeft.intensity;
            }
            weaponLightLeft.intensity = 0;
            Destroy(flareParticleSystemInstanceLeft.gameObject);
        }
        if (flareParticleSystemInstanceRight != null)
        {
            if (weaponLightIntensity < weaponLightRight.intensity)
            {
                weaponLightIntensity = weaponLightRight.intensity;
            }
            weaponLightRight.intensity = 0;
            Destroy(flareParticleSystemInstanceRight.gameObject);
        }

        STProjectile sTProjectile = projectilePrefab.GetComponent<STProjectile>();
        if (sTProjectile != null)
        {
            STProjectile projectile = Instantiate(sTProjectile);
            projectile.transform.SetParent(weapon.transform);
            projectile.transform.localPosition = new Vector3(0, projectileYCorrection, 0);
            projectile.transform.SetParent(null);
            projectile.transform.rotation = pRotation;
            projectile.InitializeSingleAttackProjectile(pTargets, pSpeed, damage);
        }
        else
        {
            throw new System.Exception("There is no STProjectile component on projectilePrefab.");
        }
    }

    private ParticleSystem InstantiateParitcleSystemPrefabs(ParticleSystem pParticleSystemPrefab, float pPositionCorrection, bool pIsLeft)
    {
        ParticleSystem particleSystemInstance = Instantiate(pParticleSystemPrefab);
        particleSystemInstance.transform.SetParent(weapon.transform);
        particleSystemInstance.transform.localPosition = new Vector3(pIsLeft ? flarePrefabPositionCorrectionXLeft : flarePrefabPositionCorrectionXRight, pPositionCorrection, 0);
        return particleSystemInstance;
    }
}
