using UnityEngine;

/// <summary>
/// Test class for pathfinding
/// </summary>
public class PathfindingTestManager : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemy; //Enemy
    [SerializeField]
    private GameObject goal; //Goal that enemy wants to reach

    private void Awake()
    {
        SetTarget();
    }

    /// <summary>
    /// Sets target of enemy
    /// </summary>
    private void SetTarget()
    {
        enemy.SetTarget(goal);
    }
}
