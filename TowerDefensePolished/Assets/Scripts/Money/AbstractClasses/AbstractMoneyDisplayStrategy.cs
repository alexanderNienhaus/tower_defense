using UnityEngine;

/// <summary>
/// Abstract class for money displaying, see tmpro money displaying strategy class for concrete implementation
/// </summary>
public abstract class AbstractMoneyDisplayStrategy : MonoBehaviour
{
    /// <summary>
    /// Abstract function of DisplayText. Will be different for each concrete wave displaying strategy
    /// </summary>
    public abstract void DisplayText(string pText);
}
