using UnityEngine;

/// <summary>
/// Debug class that triggers the game over event
/// </summary>
public class TriggerGameOver : MonoBehaviour
{
    [SerializeField]
    private GameLostEvent gameLostEvent; //Event for game lost

    /// <summary>
    /// End the game by triggering game lost event
    /// </summary>
    public void EndGame()
    {
        gameLostEvent.Raise();
    }
}
