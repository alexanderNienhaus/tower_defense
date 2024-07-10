using UnityEngine;

/// <summary>
/// Controls the game lost situation, specifically the game lost display strategy. Passes events onto it
/// </summary>
public class GameLostController : MonoBehaviour
{
    private AbstractGameLostDisplayStrategy gameLostDisplayStrategy; //Strategy for displaying the game lost screen

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Gets strategy
    /// </summary>
    private void Initialize()
    {
        gameLostDisplayStrategy = GetComponent<AbstractGameLostDisplayStrategy>();
        if (gameLostDisplayStrategy == null)
        {
            throw new System.Exception("There is no component that implements the AbstractGameLostDisplayer abstract class.");
        }
    }

    /// <summary>
    /// Listens to the game lost event and displays the game lost screen
    /// </summary>
    public void OnGameLost()
    {
        gameLostDisplayStrategy.DisplayGameLost();
    }
}
