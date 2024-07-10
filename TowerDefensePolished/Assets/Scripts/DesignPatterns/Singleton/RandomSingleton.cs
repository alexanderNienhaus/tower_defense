using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSingleton", menuName = "Scriptable Objects/Singletons/RandomSingleton")]
/// <summary>
/// Scriptable object singleton for a global list of all currently alive enemies
/// </summary>
public class RandomSingleton : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<EnemyController> enemies; //List of all currently alive enemies

    /// <summary>
    /// Returns list of all currently alive enemies
    /// </summary>
    public List<EnemyController> GetAliveEnemies()
    {
        return enemies;
    }

    /// <summary>
    /// Listens to the enemy spawns event and adds the enemy to the list
    /// </summary>
    public void OnEnemySpawns(EnemyController pEnemy)
    {
        enemies.Insert(enemies.Count, pEnemy);
    }

    /// <summary>
    /// Listens to the enemy dies event and removes the enemy from the list
    /// </summary>
    public void OnEnemyDies(int pCarriedMoney, EnemyController pEnemy)
    {
        enemies.Remove(pEnemy);
    }

    /// <summary>
    /// Listens to the enemy reaches goal event and removes the enemy from the list
    /// </summary>
    public void OnEnemyReachesGoal(int pDamage, EnemyController pEnemy)
    {
        enemies.Remove(pEnemy);
    }

    /// <summary>
    /// Resets values that were changed during runtime
    /// </summary>
    public void OnAfterDeserialize()
    {
        enemies = new List<EnemyController>();
    }

    /// <summary>
    /// No function but has to be implemented
    /// </summary>
    public void OnBeforeSerialize()
    {
    }
}
