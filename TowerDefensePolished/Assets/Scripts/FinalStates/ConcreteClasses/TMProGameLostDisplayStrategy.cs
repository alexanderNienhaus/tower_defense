using UnityEngine;
using TMPro;

/// <summary>
/// Concrete class for diplaying the game lost screen, uses TMPro
/// </summary>
public class TMProGameLostDisplayStrategy : AbstractGameLostDisplayStrategy
{
    [SerializeField]
    private GameObject endScreen; //Gameobject of the endscreen

    private TextMeshProUGUI endScreenText; //TMPro element for displaying the end screen text

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Get component
    /// </summary>
    private void Initialize()
    {
        endScreenText = endScreen.GetComponentInChildren<TextMeshProUGUI>();
        if (endScreenText == null)
        {
            throw new System.Exception("There is no TextMeshProUGUI component in children of endscreen.");
        }
    }

    /// <summary>
    /// Activates the end screen and displays the game lost string
    /// </summary>
    public override void DisplayGameLost()
    {
        endScreen.SetActive(true);
        endScreenText.SetText(gameLostString);
    }
}
