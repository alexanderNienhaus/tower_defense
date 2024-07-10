using UnityEngine;

/// <summary>
/// Abstract class for game lost display strategy
/// </summary>
public abstract class AbstractGameLostDisplayStrategy : MonoBehaviour
{
    [SerializeField]
    protected string gameLostString; //String to be displayed on the endscreen if the game is lost

    /// <summary>
    /// Abstract function DisplayGameWon. Will be different for each concrete game lost display strategy
    /// </summary>
    public abstract void DisplayGameLost();
}
