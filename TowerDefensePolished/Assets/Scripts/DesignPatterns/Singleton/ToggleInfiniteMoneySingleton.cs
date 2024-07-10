using UnityEngine;

[CreateAssetMenu(fileName = "ToggleInfiniteMoneySingleton", menuName = "Scriptable Objects/Singletons/ToggleInfiniteMoneySingleton")]
/// <summary>
/// Scriptable object singleton for a the debug option of toggling infinite money
/// </summary>
public class ToggleInfiniteMoneySingleton : ScriptableObject
{
    [SerializeField]
    private bool infiniteMoney; //Bool of inifinte money
    [SerializeField]
    private string infiniteMoneyString; //String thats displayed in the money GUI if infinite money is toggled on

    /// <summary>
    /// Returns infinite money field
    /// </summary>
    public bool GetInfiniteMoney()
    {
        return infiniteMoney;
    }

    /// <summary>
    /// Returns infinite money string field
    /// </summary>
    public string GetInfiniteMoneyString()
    {
        return infiniteMoneyString;
    }
}
