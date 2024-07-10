using UnityEngine;

/// <summary>
/// Abstract class for health displaying strategy
/// </summary>
public abstract class AbstractEnemyHealthDisplayStrategy : MonoBehaviour
{
    /// <summary>
    /// Abstract function SetHealth. Will be different for each concrete health displaying strategy
    /// </summary>
    public abstract void SetHealth(int pCurrentHealth, int maxHp);
}
