using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerChangedEvent", menuName = "Scriptable Objects/Events/Shop Events/TowerChangedEvent")]
public class TowerChangedEvent : ScriptableObject
{
    [SerializeField]
    private List<TowerChangedEventListener> listeners = new List<TowerChangedEventListener>(); //List of listeners

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
    public void RegisterListener(TowerChangedEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Removes a listener
    /// </summary>
    public void UnregisterListener(TowerChangedEventListener listener)
    {
        listeners.Remove(listener);
    }
}
