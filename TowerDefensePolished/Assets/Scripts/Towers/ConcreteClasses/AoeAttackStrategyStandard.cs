using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "AoeAttackStrategyStandard",
    menuName = "Scriptable Objects/Towers/AoeAttackStrategyStandard")]
/// <summary>
/// Concrete Scriptable object for the aoe attacker
/// </summary>
public class AoeAttackStrategyStandard : AbstractAttackStrategy
{
    [SerializeField]
    protected int damage; //Damage of projectile
    [SerializeField]
    protected AbstractImpact impactPrefab; //Prefab of aoe impact
    [SerializeField]
    private ParticleSystem lightningParticleSystemPrefab;
    [SerializeField]
    private float lightningPrefabPositionCorrectionY;
    [SerializeField]
    private float projectileYCorrection;

    private ParticleSystem lightningParticleSystemInstance;
    private Light2D weaponLight;
    private float weaponLightIntensity;


    /// <summary>
    /// Gets components and sets initial values. Rotates weapon towards middle of screen. Almost all functions may be overridden by concrete attacker
    /// </summary>
    public override void Initialize(GameObject pGameObject, Transform pTransform)
    {
        base.Initialize(pGameObject, pTransform);

        weaponLight = weapon.gameObject.transform.GetChild(0).GetComponent<Light2D>();
        weaponLight.transform.localPosition = new Vector3(0, lightningPrefabPositionCorrectionY, 0);
    }

    /// <summary>
    /// Called on start by tower controler
    /// </summary>
    public override void OnStart()
    {
        base.OnStart();

        lightningParticleSystemInstance = InstantiateParitcleSystemPrefab(lightningParticleSystemPrefab);
    }

    /// <summary>
    /// Returns damage field
    /// </summary>            
    public int GetDamage()
    {
        return damage;
    }

    public override void OnLastFrame()
    {
        if (lightningParticleSystemInstance == null)
        {
            weaponLight.intensity = weaponLightIntensity;
            lightningParticleSystemInstance = InstantiateParitcleSystemPrefab(lightningParticleSystemPrefab);
        }
    }

    public override void OnFifthFrame()
    {
        if (lightningParticleSystemInstance != null)
        {
            if (weaponLightIntensity < weaponLight.intensity)
            {
                weaponLightIntensity = weaponLight.intensity;
            }
            weaponLight.intensity = 0;
            Destroy(lightningParticleSystemInstance.gameObject);
        }
    }

    /// <summary>
    /// Concrete implemetation of CreateProjectile function. Creates an aoe projectile and sets correct rotation and position
    /// </summary>
    protected override void CreateProjectile(List<EnemyController> pTargets, Quaternion pRotation, float pSpeed)
    {
        AoeProjectile aoeProjectile = projectilePrefab.GetComponent<AoeProjectile>();
        if (aoeProjectile != null)
        {
            AoeProjectile projectile = Instantiate(aoeProjectile);
            projectile.transform.SetParent(weapon.transform);
            projectile.transform.localPosition = new Vector3(0, projectileYCorrection, 0);
            projectile.transform.SetParent(null);
            projectile.transform.rotation = pRotation;
            projectile.InitializeAoeProjectile(pTargets, pSpeed, damage, impactPrefab);
        }
        else
        {
            throw new System.Exception("There is no AoeProjectile component on projectilePrefab prefab.");
        }
    }

    private ParticleSystem InstantiateParitcleSystemPrefab(ParticleSystem pParticleSystemPrefab)
    {
        ParticleSystem particleSystemInstance = Instantiate(pParticleSystemPrefab);
        particleSystemInstance.transform.SetParent(weapon.transform);
        particleSystemInstance.transform.localPosition = new Vector3(0, lightningPrefabPositionCorrectionY, 0);
        return particleSystemInstance;
    }
}