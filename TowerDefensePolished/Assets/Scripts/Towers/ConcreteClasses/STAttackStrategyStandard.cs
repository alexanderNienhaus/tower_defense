using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu (fileName = "STAttackStrategyStandard",
    menuName = "Scriptable Objects/Towers/STAttackStrategyStandard")]
/// <summary>
/// Concrete Scriptable object for the single target attacker
/// </summary>
public class STAttackStrategyStandard : AbstractAttackStrategy
{
    [SerializeField]
    protected int damage; //Damage of projectile
    [SerializeField]
    private ParticleSystem flareParticleSystemPrefab;
    [SerializeField]
    private float flarePrefabPositionCorrectionY;
    [SerializeField]
    private float projectileYCorrection;

    private ParticleSystem flareParticleSystemInstance;
    private Light2D weaponLight;
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

        flareParticleSystemInstance = InstantiateParitcleSystemPrefab(flareParticleSystemPrefab);

        weaponLight = weapon.gameObject.transform.GetChild(0).GetComponent<Light2D>();
        weaponLight.transform.localPosition = new Vector3(0, flarePrefabPositionCorrectionY, 0);
    }

    /// <summary>
    /// Stops the animtion on first frame
    /// </summary>
    public override void OnFirstFrame()
    {
        animator.speed = 0;

        if (flareParticleSystemInstance == null)
        {
            weaponLight.intensity = weaponLightIntensity;
            flareParticleSystemInstance = InstantiateParitcleSystemPrefab(flareParticleSystemPrefab);
        }
    }

    /// <summary>
    /// Concrete implemetation of CreateProjectile function. Creates a single target projectile and sets correct rotation and position
    /// </summary>
    protected override void CreateProjectile(List<EnemyController> pTargets, Quaternion pRotation, float pSpeed)
    {
        if (flareParticleSystemInstance != null)
        {
            if (weaponLightIntensity < weaponLight.intensity)
            {
                weaponLightIntensity = weaponLight.intensity;
            }
            weaponLight.intensity = 0;
            Destroy(flareParticleSystemInstance.gameObject);
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

    private ParticleSystem InstantiateParitcleSystemPrefab(ParticleSystem pParticleSystemPrefab)
    {
        ParticleSystem particleSystemInstance = Instantiate(pParticleSystemPrefab);
        particleSystemInstance.transform.SetParent(weapon.transform);
        particleSystemInstance.transform.localPosition = new Vector3(0, flarePrefabPositionCorrectionY, 0);
        return particleSystemInstance;
    }
}
