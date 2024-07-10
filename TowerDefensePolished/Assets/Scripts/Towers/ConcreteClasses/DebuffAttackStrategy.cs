using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffAttackStrategy",
    menuName = "Scriptable Objects/Towers/DebuffAttackStrategy")]
/// <summary>
/// Concrete Scriptable object for the debuff attacker. Overrides some functions, so its weapon does not turn
/// </summary>
public class DebuffAttackStrategy : AbstractAttackStrategy
{
    [SerializeField]
    protected float slowPercentage; //Percentage of slow
    [SerializeField]
    protected float slowDuration; //Duration of slow
    [SerializeField]
    private ParticleSystem chargeParticleSystemPrefab;
    [SerializeField]
    private ParticleSystem dormentParticleSystemPrefab;

    private ParticleSystem dormentParticleSystemInstance;
    private float weaponHeightCorrection;

    public override void OnFirstFrame()
    {
        animator.speed = 0;
    }

    /// <summary>
    /// Returns slow strength field
    /// </summary>
    public float GetSlowStrength()
    {
        return slowPercentage;
    }

    /// <summary>
    /// Returns slow slowDuration field
    /// </summary> 
    public float GetSlowDuration()
    {
        return slowDuration;
    }

    /// <summary>
    /// Concrete override implemetation of Initialize function. Does not rotate the weapon
    /// </summary>
    public override void Initialize(GameObject pGameObject, Transform pTransform)
    {
        base.Initialize(pGameObject, pTransform);

        SpriteRenderer weaponSr = weapon.GetComponent<SpriteRenderer>();
        if (weaponSr == null)
        {
            throw new System.Exception("There is no SpriteRenderer component in weapon.");
        }
        weaponHeightCorrection = weaponSr.bounds.size.y / 6f;
        dormentParticleSystemInstance = InstantiateParitcleSystemPrefab(dormentParticleSystemPrefab);
    }

    /// <summary>
    /// Overrides OnStart method so the weapon does not turn
    /// </summary>
    public override void OnStart()
    {

    }

    /// <summary>
    /// Concrete implemetation of OnUpdate function. Checks for target but does not rotate weapon
    /// </summary>
    public override void OnUpdate()
    {
        if (weapon == null || animator == null)
            return;

        if (currentTarget == null || !CheckInRange(currentTarget.transform.position))
        {
            if (!SearchNewTarget())
            {
                currentTarget = null;
                if (dormentParticleSystemInstance == null)
                    dormentParticleSystemInstance = InstantiateParitcleSystemPrefab(dormentParticleSystemPrefab);
            }
        }

        if (currentTarget != null)
        {
            if (animator.speed == 0)
            {
                if(dormentParticleSystemInstance != null)
                    Destroy(dormentParticleSystemInstance.gameObject);
                InstantiateParitcleSystemPrefab(chargeParticleSystemPrefab);
            }
            animator.speed = attackSpeed;
        }
    }

    /// <summary>
    /// Concrete implemetation of CreateProjectile function. Creates a debuff projectile and sets correct position
    /// </summary>
    protected override void CreateProjectile(List<EnemyController> pTargets, Quaternion pRotation, float pSpeed)
    {
        DebuffProjectile debuffProjectile = projectilePrefab.GetComponent<DebuffProjectile>();
        if (debuffProjectile != null)
        {
            DebuffProjectile projectile = Instantiate(debuffProjectile);
            projectile.transform.position = weapon.position;
            projectile.InitializeDebuffProjectile(pTargets, pSpeed, slowPercentage, slowDuration);
        }
        else
        {
            throw new System.Exception("There is no DebuffProjectile component on projectilePrefab.");
        }
    }

    private ParticleSystem InstantiateParitcleSystemPrefab(ParticleSystem pParticleSystemPrefab)
    {
        Vector3 position = weapon.position;
        position.y += weaponHeightCorrection;
        ParticleSystem particleSystemInstance = Instantiate(pParticleSystemPrefab, position, Quaternion.identity);
        particleSystemInstance.transform.SetParent(gameObject.transform);
        return particleSystemInstance;
    }
}
