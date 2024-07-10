using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// Response for the enemy spawns event
/// </summary>
public class EnemySpawnsUnityEvent : UnityEvent<EnemyController> { }

/// <summary>
/// Listener for the enemy spawns event
/// </summary>
public class EnemySpawnsEventListener : MonoBehaviour
{
    public EnemySpawnsUnityEvent Response; //Response to the event

    [SerializeField]
    private EnemySpawnsEvent Event; //Event to listen to

    /// <summary>
    /// Invoke response if event is raised
    /// </summary>
    public void OnEventRaised(EnemyController pEnemyController)
    {
        Response.Invoke(pEnemyController);
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
