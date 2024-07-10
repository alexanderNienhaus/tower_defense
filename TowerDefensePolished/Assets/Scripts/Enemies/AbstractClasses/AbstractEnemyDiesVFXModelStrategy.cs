using UnityEngine;

/// <summary>
/// Abstract class for enemy dies vfx model strategy. Contains the vfx displaying strategy and creates the vfx element
/// </summary>
public abstract class AbstractEnemyDiesVFXModelStrategy : MonoBehaviour
{
    [SerializeField]
    protected AbstractEnemyDiesVFXPrefab enemyDiesVFXDisplayingStrategyPrefab; //Prefab of vfx elemenent
    [SerializeField]
    protected string enemyDiesVFXText; //Element for displaying enemy dies vfx text
    [SerializeField]
    protected float timeToLive; //Time until the vfx gets destroyed
    [SerializeField]
    protected Canvas canvas; //Canvas

    /// <summary>
    /// Abstract function DisplayEnemyDiesVFX. Will be different for each concrete enemy dies vxf model strategy
    /// </summary>
    public abstract void CreateEnemyDiesVFX(int pMoney, Vector3 pPosition);
}
