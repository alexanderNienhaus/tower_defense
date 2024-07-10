using UnityEngine;

/// <summary>
/// Abstract class for shop info displaying, see tmpro shop info displaying strategy class for concrete implementation
/// </summary>
public abstract class AbstractShopInfoDisplayStrategy : MonoBehaviour
{
    /// <summary>
    /// Abstract function of DisplayText. Will be different for each concrete shop info displaying strategy
    /// </summary>
    public abstract void DisplayText(string pText, float pTotalTimeToLive);
}
