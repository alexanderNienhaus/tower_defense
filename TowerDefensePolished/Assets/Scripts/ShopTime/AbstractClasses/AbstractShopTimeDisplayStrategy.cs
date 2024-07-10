using UnityEngine;

/// <summary>
/// Abstract class for time displaying, see tmpro shop time displaying strategy class for concrete implementation
/// </summary>
public abstract class AbstractShopTimeDisplayStrategy : MonoBehaviour
{
    /// <summary>
    /// Abstract function of DisplayText. Will be different for each concrete time displaying strategy
    /// </summary>
    public abstract void DisplayText(string pText);
}
