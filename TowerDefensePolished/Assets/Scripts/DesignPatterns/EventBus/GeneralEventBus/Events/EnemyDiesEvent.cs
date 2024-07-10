using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDiesEvent", menuName = "Scriptable Objects/Events/General Events/EnemyDiesEvent")]
/// <summary>
/// Scriptable object for enemy dies event, contains the carried money and the enemy controller component of the enemy that died.
/// Raised by enemy controller if an enemy dies
/// Listened to by tower controller, wave controller and money controller
/// </summary>
public class EnemyDiesEvent : ScriptableObject
{
    [SerializeField]
    private List<EnemyDiesEventListener> listeners = new List<EnemyDiesEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise(int pCarriedMoney, EnemyController pEnemyController)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pCarriedMoney, pEnemyController);
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(EnemyDiesEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(EnemyDiesEventListener listener)
    {
        listeners.Remove(listener);
    }
}
