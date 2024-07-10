using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// Response for the enemy dies event
/// </summary>
public class EnemyDiesUnityEvent : UnityEvent<int, EnemyController> { }

/// <summary>
/// Listener for the enemy dies event
/// </summary>
public class EnemyDiesEventListener : MonoBehaviour
{
    public EnemyDiesUnityEvent Response; //Response to the event

    [SerializeField]
    private EnemyDiesEvent Event; //Event to listen to

    /// <summary>
    /// Invoke response if event is raised
    /// </summary>
    public void OnEventRaised(int pCarriedMoney, EnemyController pEnemyController)
    {
        Response.Invoke(pCarriedMoney, pEnemyController);
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
