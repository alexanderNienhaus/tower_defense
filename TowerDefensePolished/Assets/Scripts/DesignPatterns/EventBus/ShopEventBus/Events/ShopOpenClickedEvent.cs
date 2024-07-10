using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopOpenClickedEvent", menuName = "Scriptable Objects/Events/Shop Events/ShopOpenClickedEvent")]
/// <summary>
/// Scriptable object for shop open event, contains the tower position that was selected, and if a tower was already present there, its
/// tower controller component
/// Raised by shop opne controller if the shop is accessed
/// Listened to by time controller
/// </summary>
public class ShopOpenClickedEvent : ScriptableObject
{
    [SerializeField]
    private List<ShopOpenClickedEventListener> listeners = new List<ShopOpenClickedEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise(Vector3 pPosition, TowerController pTower = null)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pPosition, pTower);
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(ShopOpenClickedEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(ShopOpenClickedEventListener listener)
    {
        listeners.Remove(listener);
    }
}
