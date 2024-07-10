using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyReachesGoalEvent", menuName = "Scriptable Objects/Events/General Events/EnemyReachesGoalEvent")]
/// <summary>
/// Scriptable object for enemy reaches goal event, contains the damage and the enemy controller component of the enemy that reached the goal
/// Raised by enemy controller if an enemy reaches the goal
/// Listened to by gealth controller, wave controller and tower controller
/// </summary>
public class EnemyReachesGoalEvent : ScriptableObject
{
    [SerializeField]
    private List<EnemyReachesGoalEventListener> listeners = new List<EnemyReachesGoalEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise(int pDamage, EnemyController pEnemyController)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pDamage, pEnemyController);
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(EnemyReachesGoalEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(EnemyReachesGoalEventListener listener)
    {
        listeners.Remove(listener);
    }
}
