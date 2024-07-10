using UnityEngine;

/// <summary>
/// Helper class that quits the game
/// </summary>
public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// Quits game
    /// </summary>
    public void QuitGameBtnClicked()
    {
        Application.Quit();
    }
}
