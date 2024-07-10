using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TowerChangedUnityEvent :  UnityEvent<Vector3, TowerController, ShopAction> { }

public class TowerChangedEventListener : MonoBehaviour
{
    public TowerChangedUnityEvent Response; //Response to the event

    [SerializeField]
    private TowerChangedEvent Event; //Event to listen to

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
