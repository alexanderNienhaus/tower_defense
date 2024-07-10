using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// Response for the shop open clicked event
/// </summary>
public class ShopOpenClickedUnityEvent : UnityEvent<Vector3, TowerController> { }

/// <summary>
/// Listener for the shop open clicked event
/// </summary>
public class ShopOpenClickedEventListener : MonoBehaviour
{
    public ShopOpenClickedUnityEvent Response; //Response to the event

    [SerializeField]
    private ShopOpenClickedEvent Event; //Event to listen to

    /// <summary>
    /// Invoke response if event is raised
    /// </summary>
    public void OnEventRaised(Vector3 pPosition, TowerController pTower)
    {
        Response.Invoke(pPosition, pTower);
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
