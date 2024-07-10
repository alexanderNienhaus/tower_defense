using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnsEvent", menuName = "Scriptable Objects/Events/General Events/EnemySpawnsEvent")]
/// <summary>
/// Scriptable object for enemy spawns event, contains the enemy controller component of the enemy that spawned
/// Raised by enemy controller if an enemy spawns
/// Listened to by tower controller
/// </summary>
public class EnemySpawnsEvent : ScriptableObject
{
    [SerializeField]
    private List<EnemySpawnsEventListener> listeners = new List<EnemySpawnsEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise(EnemyController pEnemyController)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pEnemyController);
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(EnemySpawnsEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(EnemySpawnsEventListener listener)
    {
        listeners.Remove(listener);
    }
}
