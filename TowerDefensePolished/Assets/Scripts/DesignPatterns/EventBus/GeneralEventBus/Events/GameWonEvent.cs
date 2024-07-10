using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameWonEvent", menuName = "Scriptable Objects/Events/General Events/GameWonEvent")]
/// <summary>
/// Scriptable object for game won event, contains no data
/// Raised by wave controller if the last wave was defeated
/// Listened to by game won controller
/// </summary>
public class GameWonEvent : ScriptableObject
{
    [SerializeField]
    private List<GameWonEventListener> listeners = new List<GameWonEventListener>(); //List of listeners

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
    public void RegisterListener(GameWonEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(GameWonEventListener listener)
    {
        listeners.Remove(listener);
    }
}
