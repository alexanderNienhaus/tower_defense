using System.Collections.Generic;

/// <summary>
/// Concrete class for aoe impacts
/// </summary>
public class BasicAoeImpact : AbstractImpact
{
    protected int damage; //Damage of the impact

    /// <summary>
    /// Initialize values
    /// </summary>
    public void InitializeBasicAoeImpact(List<EnemyController> pTargets, int pDamage)
    {
        targets = pTargets;
        damage = pDamage;
    }

    /// <summary>
    /// Does damage to an enemy but only if that enemy was not already hit 
    /// </summary>
    protected override void EnemyHit(EnemyController pEnemyController)
    {
        if (enemiesHit.Contains(pEnemyController))
            return;
        enemiesHit.Add(pEnemyController);
        pEnemyController.DoDamage(damage);
    }
}
