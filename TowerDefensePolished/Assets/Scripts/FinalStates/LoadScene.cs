using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Helper class that loads a scene
/// </summary>
public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoadName; //Name of scene to be loaded

    /// <summary>
    /// Load scene
    /// </summary>
    public void Load()
    {
        SceneManager.LoadScene(sceneToLoadName);
    }
}
