using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// Response for the enough money event
/// </summary>
public class EnoughMoneyUnityEvent :  UnityEvent<Vector3, TowerController, ShopAction> { }

/// <summary>
/// Listener for the enough money event
/// </summary>
public class EnoughMoneyEventListener : MonoBehaviour
{
    public EnoughMoneyUnityEvent Response; //Response to the event

    [SerializeField]
    private EnoughMoneyEvent Event; //Event to listen to

    /// <summary>
    /// Invoke response if event is raised
    /// </summary>
    public void OnEventRaised(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        Response.Invoke(pPosition, pTower, pShopAction);
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
