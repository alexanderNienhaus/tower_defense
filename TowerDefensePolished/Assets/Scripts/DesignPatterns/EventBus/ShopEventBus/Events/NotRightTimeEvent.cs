using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotRightTimeEvent", menuName = "Scriptable Objects/Events/Shop Events/NotRightTimeEvent")]
/// <summary>
/// Scriptable object for not right ime event, contains no data
/// Raised by time controller if the shop is accessed during a wave
/// Listened to by shop info controller
/// </summary>
public class NotRightTimeEvent : ScriptableObject
{
    [SerializeField]
    private List<NotRightTimeEventListener> listeners = new List<NotRightTimeEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(NotRightTimeEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(NotRightTimeEventListener listener)
    {
        listeners.Remove(listener);
    }
}
