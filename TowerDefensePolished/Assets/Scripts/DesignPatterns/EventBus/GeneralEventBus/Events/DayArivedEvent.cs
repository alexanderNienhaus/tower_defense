using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayArivedEvent", menuName = "Scriptable Objects/Events/General Events/DayArivedEvent")]
/// <summary>
/// Scriptable object for wave over event, contains no data
/// Raised by wave controller if a wave (exept the last one) is defeated
/// Listened to by time controller
/// </summary>
public class DayArivedEvent : ScriptableObject
{
    [SerializeField]
    private List<DayyArivedEventListener> listeners = new List<DayyArivedEventListener>(); //List of listeners

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
    public void RegisterListener(DayyArivedEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(DayyArivedEventListener listener)
    {
        listeners.Remove(listener);
    }
}
