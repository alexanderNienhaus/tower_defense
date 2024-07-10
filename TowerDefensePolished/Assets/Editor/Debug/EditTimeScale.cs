using UnityEngine;

/// <summary>
/// Debug class that edits the time scale
/// </summary>
public class EditTimeScale : MonoBehaviour
{
    [SerializeField]
    private float timeScale = 1f; //Time scale. Used to speed up or slow down game

    private void Update()
    {
        UpdateTimeScale();
    }

    /// <summary>
    /// Updates the time scale at runtime
    /// </summary>
    private void UpdateTimeScale()
    {
        Time.timeScale = timeScale;
    }
}
