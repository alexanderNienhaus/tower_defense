using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Concrete class for aoe projectiles. Instantiates an aoe impact from its prefab upon collision
/// </summary>
public class AoeProjectile : AbstractProjectile
{
    private int damage; //Damage of projectile
    private AbstractImpact impactPrefab; //Prefab of impact

    /// <summary>
    /// Initialize values
    /// </summary>
    public void InitializeAoeProjectile(List<EnemyController> pTargets, float pSpeed, int pDamage, AbstractImpact pImpactPrefab)
    {
        targets = pTargets;
        damage = pDamage;
        speed = pSpeed;
        impactPrefab = pImpactPrefab;
    }

    /// <summary>
    /// Checks for a collision with all alive enemies
    /// </summary>
    protected override void EnemyHit(EnemyController pEnemyController)
    {
        BasicAoeImpact basicAoeImpact = impactPrefab.GetComponent<BasicAoeImpact>();
        if (basicAoeImpact != null)
        {
            BasicAoeImpact impact = Instantiate<BasicAoeImpact>(basicAoeImpact);
            impact.transform.position = transform.position;
            impact.InitializeBasicAoeImpact(targets, damage);
            Destroy(gameObject);
        } else
        {
            throw new System.Exception("There is no BasicAoeImpact component on impact prefab.");
        }
    }
}
