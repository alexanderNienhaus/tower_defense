using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadScene))]
/// <summary>
/// Helper class for creating a button in the inspector
/// </summary>
public class CustomInspectorLoadScene : Editor
{
    /// <summary>
    /// Create a button that calls the load function of the load scene class
    /// </summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LoadScene loadScene = (LoadScene)target;
        if (GUILayout.Button("Load Scene"))
        {
            loadScene.Load();
        }
    }
}
