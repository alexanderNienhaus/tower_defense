using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLostEvent", menuName = "Scriptable Objects/Events/General Events/GameLostEvent")]
/// <summary>
/// Scriptable object for game lost event, contains no data
/// Raised by health controller if health reaches 0
/// Listened to by game lost controller
/// </summary>
public class GameLostEvent : ScriptableObject
{
    [SerializeField]
    private List<GameLostEventListener> listeners = new List<GameLostEventListener>(); //List of listeners

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
    public void RegisterListener(GameLostEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(GameLostEventListener listener)
    {
        listeners.Remove(listener);
    }
}
