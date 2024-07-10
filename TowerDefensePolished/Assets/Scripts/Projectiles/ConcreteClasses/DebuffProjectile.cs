using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Concrete class for debuff projectiles
/// </summary>
public class DebuffProjectile : AbstractProjectile
{
    protected float slowPercentage; //Percentage of slow
    protected float slowDuration; //Duration of slow
    protected List<EnemyController> enemiesHit; //List of enemies that have already been hit

    /// <summary>
    /// Initialize values
    /// </summary>
    public void InitializeDebuffProjectile(List<EnemyController> pTargets, float pSpeed, float pSlowPercentage, float pSlowDuration)
    {
        targets = pTargets;
        speed = pSpeed;
        slowPercentage = pSlowPercentage;
        slowDuration = pSlowDuration;
        enemiesHit = new List<EnemyController>();
    }

    /// <summary>
    /// Overrides FixedUpdate so the projectile does not move
    /// </summary>
    protected override void FixedUpdate()
    {
        CheckForHit();
    }

    /// <summary>
    /// Applies slow to an enemy but only if that enemy was not already hit 
    /// </summary>
    protected override void EnemyHit(EnemyController pEnemyController)
    {
        if (enemiesHit.Contains(pEnemyController))
            return;
        enemiesHit.Add(pEnemyController);
        pEnemyController.DoSlow(slowPercentage, slowDuration);
    }

    /// <summary>
    /// Destroys the projectile if the animation is over
    /// </summary>
    protected void DebuffAnimationOver()
    {
        Destroy(gameObject);
    }
}
