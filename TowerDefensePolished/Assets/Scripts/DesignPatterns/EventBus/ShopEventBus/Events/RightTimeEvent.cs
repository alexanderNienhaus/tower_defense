using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RightTimeEvent", menuName = "Scriptable Objects/Events/Shop Events/RightTimeEvent")]
/// <summary>
/// Scriptable object for right time event, contains the tower position that was selected and the tower controller component of the specific tower 
/// Raised by time controller if the shop is accessed during shop time
/// Listened to by shop info controller
/// </summary>
public class RightTimeEvent : ScriptableObject
{
    [SerializeField]
    private List<RightTimeEventListener> listeners = new List<RightTimeEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise(Vector3 pPosition, TowerController pTower)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pPosition, pTower);
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(RightTimeEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(RightTimeEventListener listener)
    {
        listeners.Remove(listener);
    }
}
