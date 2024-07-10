using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotEnoughMoneyEvent", menuName = "Scriptable Objects/Events/Shop Events/NotEnoughMoneyEvent")]
/// <summary>
/// Scriptable object for not enough money event, contains the specific tower type, level, and shop action
/// Raised by money controller if the current money is not enough for the specific shop action
/// Listened to by shop info controller
/// </summary>
public class NotEnoughMoneyEvent : ScriptableObject
{
    [SerializeField]
    private List<NotEnoughMoneyEventListener> listeners = new List<NotEnoughMoneyEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise(ShopAction pShopAction, TowerType pTowerType, TowerLevel pTowerLevel)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pShopAction, pTowerType, pTowerLevel);
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(NotEnoughMoneyEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(NotEnoughMoneyEventListener listener)
    {
        listeners.Remove(listener);
    }
}
