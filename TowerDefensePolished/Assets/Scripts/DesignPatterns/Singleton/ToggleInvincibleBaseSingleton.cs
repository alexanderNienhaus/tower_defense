using UnityEngine;

[CreateAssetMenu(fileName = "ToggleInvincibleBaseSingleton", menuName = "Scriptable Objects/Singletons/ToggleInvincibleBaseSingleton")]
/// <summary>
/// Scriptable object singleton for a the debug option of toggling invincible base
/// </summary>

public class ToggleInvincibleBaseSingleton : ScriptableObject
{
    [SerializeField]
    private bool invincibleBase; //Bool of invincible base
    [SerializeField]
    private string infiniteHealthString; //String thats displayed in the health GUI if invincible base is toggled on

    /// <summary>
    /// Returns invincible base field
    /// </summary>
    public bool GetInvincibleBase()
    {
        return invincibleBase;
    }

    /// <summary>
    /// Returns infinite health string field
    /// </summary>
    public string GetInfiniteHealthString()
    {
        return infiniteHealthString;
    }
}
