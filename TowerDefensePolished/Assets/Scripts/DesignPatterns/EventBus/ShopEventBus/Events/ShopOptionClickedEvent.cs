using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopOptionClickedEvent", menuName = "Scriptable Objects/Events/Shop Events/ShopOptionClickedEvent")]
/// <summary>
/// Scriptable object for shop option clicked event, contains the tower position that was seleceted, the tower controller component of the specific tower
/// and the shop action that will be excecuted
/// Raised by shop option controller if a shop option was clicked
/// Listened to by money controller
/// </summary>
public class ShopOptionClickedEvent : ScriptableObject
{
    [SerializeField]
    private List<ShopOptionClickedEventListener> listeners = new List<ShopOptionClickedEventListener>(); //List of listeners

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
    public void RegisterListener(ShopOptionClickedEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(ShopOptionClickedEventListener listener)
    {
        listeners.Remove(listener);
    }
}
