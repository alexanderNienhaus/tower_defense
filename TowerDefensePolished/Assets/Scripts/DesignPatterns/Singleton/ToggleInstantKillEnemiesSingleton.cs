using UnityEngine;

[CreateAssetMenu(fileName = "ToggleInstantKillEnemiesSingleton", menuName = "Scriptable Objects/Singletons/ToggleInstantKillEnemiesSingleton")]
/// <summary>
/// Scriptable object singleton for a the debug option of instant killing enemies
/// </summary>
public class ToggleInstantKillEnemiesSingleton : ScriptableObject
{
    [SerializeField]
    private bool instantKillEnemies; //Bool of instant kill enemies

    /// <summary>
    /// Returns instant kill enemies field
    /// </summary>
    public bool GetInstantKillEnemies()
    {
        return instantKillEnemies;
    }
}
