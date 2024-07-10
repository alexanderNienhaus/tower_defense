using System.Collections.Generic;

/// <summary>
/// Concrete class for single attack projectiles
/// </summary>
public class STProjectile : AbstractProjectile
{
    protected int damage; //Damage of projectile

    /// <summary>
    /// Initializes values
    /// </summary>
    public void InitializeSingleAttackProjectile(List<EnemyController> pTargets, float pSpeed, int pDamage)
    {
        targets = pTargets;
        damage = pDamage;
        speed = pSpeed;
    }

    /// <summary>
    /// Destroys projectile and does damage to enemy
    /// </summary>
    protected override void EnemyHit(EnemyController pEnemyController)
    {
        Destroy(gameObject);
        pEnemyController.DoDamage(damage);
    }
}
