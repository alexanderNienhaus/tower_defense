using UnityEngine;

/// <summary>
/// Abstract class for game won display strategy
/// </summary>
public abstract class AbstractGameWonDisplayStrategy : MonoBehaviour
{
    [SerializeField]
    protected string gameWonString; //String to be displayed on the endscreen if the game is won

    /// <summary>
    /// Abstract function DisplayGameWon. Will be different for each concrete game won display strategy
    /// </summary>
    public abstract void DisplayGameWon();
}
