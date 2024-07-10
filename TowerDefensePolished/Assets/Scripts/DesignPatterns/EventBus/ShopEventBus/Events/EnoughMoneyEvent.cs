using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnoughMoneyEvent", menuName = "Scriptable Objects/Events/Shop Events/EnoughMoneyEvent")]
/// <summary>
/// Scriptable object for enough money event, contains the tower position that was seleceted, the tower controller component of the specific tower
/// and the shop action that will be excecuted
/// Raised by money controller if the current money is enough for the specific shop action
/// Listened to by shop do action controller and shop info controller
/// </summary>
public class EnoughMoneyEvent : ScriptableObject
{
    [SerializeField]
    private List<EnoughMoneyEventListener> listeners = new List<EnoughMoneyEventListener>(); //List of listeners

    /// <summary>
    /// Calls the on event raised funtion for all listeners
    /// </summary>
    public void Raise(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(pPosition, pTower, pShopAction);
    }

    /// <summary>
    /// Adds a listener
    /// </summary>
    public void RegisterListener(EnoughMoneyEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(EnoughMoneyEventListener listener)
    {
        listeners.Remove(listener);
    }
}
