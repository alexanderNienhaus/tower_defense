using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveOverEvent", menuName = "Scriptable Objects/Events/General Events/WaveOverEvent")]
/// <summary>
/// Scriptable object for wave over event, contains no data
/// Raised by wave controller if a wave (exept the last one) is defeated
/// Listened to by time controller
/// </summary>
public class WaveOverEvent : ScriptableObject
{
    [SerializeField]
    private List<WaveOverEventListener> listeners = new List<WaveOverEventListener>(); //List of listeners

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
    public void RegisterListener(WaveOverEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(WaveOverEventListener listener)
    {
        listeners.Remove(listener);
    }
}
