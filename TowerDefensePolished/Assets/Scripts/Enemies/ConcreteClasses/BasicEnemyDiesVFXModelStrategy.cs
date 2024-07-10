using UnityEngine;

/// <summary>
/// Concrete class for creating the enemy dies vfx
/// </summary>
public class BasicEnemyDiesVFXModelStrategy : AbstractEnemyDiesVFXModelStrategy
{
    /// <summary>
    /// Concrete implementation of DisplayEnemyDiesVFX method, instantiates a vfx element from its prefab and formats its text
    /// </summary>
    public override void CreateEnemyDiesVFX(int pMoney, Vector3 pPosition)
    {
        string str = string.Format(enemyDiesVFXText, pMoney);
        TMProEnemyDiesVFXPrefab enemyDiesVFXPrefab = Instantiate((TMProEnemyDiesVFXPrefab)enemyDiesVFXDisplayingStrategyPrefab, canvas.transform);
        enemyDiesVFXPrefab.DisplayText(str, pPosition, timeToLive);
    }
}
