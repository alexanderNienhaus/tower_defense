using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "AoeAttackStrategyUpgraded",
    menuName = "Scriptable Objects/Towers/AoeAttackStrategyUpgraded")]
/// <summary>
/// Concrete Scriptable object for the aoe attacker
/// </summary>
public class AoeAttackStrategyUpgraded : AbstractAttackStrategy
{
    [SerializeField]
    protected int damage; //Damage of projectile
    [SerializeField]
    protected AbstractImpact impactPrefab; //Prefab of aoe impact
    [SerializeField]
    private ParticleSystem lightningParticleSystemPrefab;
    [SerializeField]
    private float lightningPrefabPositionCorrectionOne;
    [SerializeField]
    private float lightningPrefabPositionCorrectionTwo;
    [SerializeField]
    private float projectileYCorrection;

    private ParticleSystem lightningParticleSystemInstanceOne;
    private ParticleSystem lightningParticleSystemInstanceTwo;
    private Light2D weaponLightOne;
    private Light2D weaponLightTwo;
    private float weaponLightIntensity;

    /// <summary>
    /// Gets components and sets initial values. Rotates weapon towards middle of screen. Almost all functions may be overridden by concrete attacker
    /// </summary>
    public override void Initialize(GameObject pGameObject, Transform pTransform)
    {
        base.Initialize(pGameObject, pTransform);

        weaponLightOne = weapon.gameObject.transform.GetChild(0).GetComponent<Light2D>();
        weaponLightOne.transform.localPosition = new Vector3(0, lightningPrefabPositionCorrectionOne, 0);
        weaponLightTwo = weapon.gameObject.transform.GetChild(1).GetComponent<Light2D>();
        weaponLightTwo.transform.localPosition = new Vector3(0, lightningPrefabPositionCorrectionTwo, 0);
    }

    /// <summary>
    /// Called on start by tower controler
    /// </summary>
    public override void OnStart()
    {
        base.OnStart();

        lightningParticleSystemInstanceOne = InstantiateParitcleSystemPrefabs(lightningParticleSystemPrefab, lightningPrefabPositionCorrectionOne);
        lightningParticleSystemInstanceTwo = InstantiateParitcleSystemPrefabs(lightningParticleSystemPrefab, lightningPrefabPositionCorrectionTwo);
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
        if (lightningParticleSystemInstanceOne == null)
        {
            weaponLightOne.intensity = weaponLightIntensity;
            lightningParticleSystemInstanceOne = InstantiateParitcleSystemPrefabs(lightningParticleSystemPrefab, lightningPrefabPositionCorrectionOne);
        }
        if (lightningParticleSystemInstanceTwo == null)
        {
            weaponLightTwo.intensity = weaponLightIntensity;
            lightningParticleSystemInstanceTwo = InstantiateParitcleSystemPrefabs(lightningParticleSystemPrefab, lightningPrefabPositionCorrectionTwo);
        }
    }

    public override void OnFifthFrame()
    {
        if (lightningParticleSystemInstanceOne != null)
        {
            if (weaponLightIntensity < weaponLightOne.intensity)
            {
                weaponLightIntensity = weaponLightOne.intensity;
            }
            weaponLightOne.intensity = 0;
            Destroy(lightningParticleSystemInstanceOne.gameObject);
        }
        if (lightningParticleSystemInstanceTwo != null)
        {
            if (weaponLightIntensity < weaponLightTwo.intensity)
            {
                weaponLightIntensity = weaponLightTwo.intensity;
            }
            weaponLightTwo.intensity = 0;
            Destroy(lightningParticleSystemInstanceTwo.gameObject);
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

    private ParticleSystem InstantiateParitcleSystemPrefabs(ParticleSystem pParticleSystemPrefab, float pPositionCorrection)
    {
        ParticleSystem particleSystemInstance = Instantiate(pParticleSystemPrefab);
        particleSystemInstance.transform.SetParent(weapon.transform);
        particleSystemInstance.transform.localPosition = new Vector3(0, pPositionCorrection, 0);
        return particleSystemInstance;
    }
}
