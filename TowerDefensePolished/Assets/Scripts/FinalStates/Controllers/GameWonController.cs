using UnityEngine;

/// <summary>
/// Controls the game won situation, specifically the game won display strategy. Passes events onto it
/// </summary>
public class GameWonController : MonoBehaviour
{
    private AbstractGameWonDisplayStrategy gameWonDisplayStrategy; //Strategy for displaying the game won screen

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Gets strategy
    /// </summary>
    private void Initialize()
    {
        gameWonDisplayStrategy = GetComponent<AbstractGameWonDisplayStrategy>();
        if (gameWonDisplayStrategy == null)
        {
            throw new System.Exception("There is no component that implements the AbstractGameWonDisplayer abstract class.");
        }
    }

    /// <summary>
    /// Listens to the game won event and displays the game won screen
    /// </summary>
    public void OnGameWon()
    {
        gameWonDisplayStrategy.DisplayGameWon();
    }
}
