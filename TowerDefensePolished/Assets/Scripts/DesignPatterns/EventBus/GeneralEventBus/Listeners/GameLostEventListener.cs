using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listener for the game lost event
/// </summary>
public class GameLostEventListener : MonoBehaviour
{
    public UnityEvent Response; //Response to the event

    [SerializeField]
    private GameLostEvent Event; //Event to listen to

    /// <summary>
    /// Invoke response if event is raised
    /// </summary>
    public void OnEventRaised()
    {
        Response.Invoke();
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
