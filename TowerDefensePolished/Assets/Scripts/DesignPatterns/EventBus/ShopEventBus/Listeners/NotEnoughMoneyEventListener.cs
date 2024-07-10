using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// Response for the not enough money event
/// </summary>
public class NotEnoughMoneyUnityEvent : UnityEvent<ShopAction, TowerType, TowerLevel> { }

/// <summary>
/// Listener for the not enough money event
/// </summary>
public class NotEnoughMoneyEventListener : MonoBehaviour
{
    public NotEnoughMoneyUnityEvent Response; //Response to the event

    [SerializeField]
    private NotEnoughMoneyEvent Event; //Event to listen to

    /// <summary>
    /// Invoke response if event is raised
    /// </summary>
    public void OnEventRaised(ShopAction pShopAction, TowerType pTowerType, TowerLevel pTowerLevel)
    {
        Response.Invoke(pShopAction, pTowerType, pTowerLevel);
    }

    /// <summary>
    /// Register listeners to the event
    /// </summary>
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    /// <summary>
    /// Unregister listeners to the event
    /// </summary>
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
}
