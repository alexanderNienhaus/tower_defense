using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// Response for the shop option clicked event
/// </summary>
public class ShopOptionClickedUnityEvent : UnityEvent<Vector3, TowerController, ShopAction> { }

/// <summary>
/// Listener for the shop option clicked event
/// </summary>
public class ShopOptionClickedEventListener : MonoBehaviour
{
    public ShopOptionClickedUnityEvent Response; //Response to the event

    [SerializeField]
    private ShopOptionClickedEvent Event; //Event to listen to

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
