using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NightArivedEvent", menuName = "Scriptable Objects/Events/General Events/NightArivedEvent")]
public class NightArivedEvent : ScriptableObject
{
    [SerializeField]
    private List<NightArivedEventListener> listeners = new List<NightArivedEventListener>(); //List of listeners

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
    public void RegisterListener(NightArivedEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(NightArivedEventListener listener)
    {
        listeners.Remove(listener);
    }
}
