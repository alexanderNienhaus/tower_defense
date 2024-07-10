using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggerGameOver))]
/// <summary>
/// Helper class for creating a button in the inspector
/// </summary>
public class CustomInspectorTriggerGameOver : Editor
{
    /// <summary>
    /// Create a button that calls the end game function of the trigger game over class
    /// </summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TriggerGameOver triggerGameOver = (TriggerGameOver)target;
        if (GUILayout.Button("Trigger Gameover"))
        {
            triggerGameOver.EndGame();
        }
    }
}
