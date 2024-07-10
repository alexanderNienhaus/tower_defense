using UnityEngine;

/// <summary>
/// Abstract class for path finding strategy
/// </summary>
public abstract class AbstractPathFindingStrategy : ScriptableObject
{
    /// <summary>
    /// Abstract function of Initialize. Will be different for each concrete path finding strategy
    /// </summary>
    public abstract void Initialize(GameObject pGameObject);

    /// <summary>
    /// Abstract function of MoveTowardsTarget. Will be different for each concrete path finding strategy
    /// </summary>
    public abstract void MoveTowardsTarget(Vector3 pTargetPosition, float pSpeed);
}
