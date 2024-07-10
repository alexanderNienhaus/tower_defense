using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopTimeOverEvent", menuName = "Scriptable Objects/Events/General Events/ShopTimeOverEvent")]
/// <summary>
/// Scriptable object for shop time overs event, contains no data
/// Raised by time controller if the shop time reaches 0
/// Listened to by wave controller
/// </summary>
public class ShopTimeOverEvent : ScriptableObject
{
    [SerializeField]
    private List<ShopTimeOverEventListener> listeners = new List<ShopTimeOverEventListener>(); //List of listeners

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
    public void RegisterListener(ShopTimeOverEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(ShopTimeOverEventListener listener)
    {
        listeners.Remove(listener);
    }
}
