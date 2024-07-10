using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
/// <summary>
/// Response for the enemy reaches goal event
/// </summary>
public class EnemyReachesGoalUnityEvent : UnityEvent<int, EnemyController> { }

/// <summary>
/// Listener for the enemy reaches goal event
/// </summary>
public class EnemyReachesGoalEventListener : MonoBehaviour
{
    public EnemyReachesGoalUnityEvent Response; //Response to the event

    [SerializeField]
    private EnemyReachesGoalEvent Event; //Event to listen to

    /// <summary>
    /// Invoke response if event is raised
    /// </summary>
    public void OnEventRaised(int pDamage, EnemyController pEnemyController)
    {
        Response.Invoke(pDamage, pEnemyController);
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
