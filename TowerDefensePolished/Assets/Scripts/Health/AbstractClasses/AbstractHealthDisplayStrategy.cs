using UnityEngine;

/// <summary>
/// Abstract class for health displaying, see tmpro health displaying strategy class for concrete implementation
/// </summary>
public abstract class AbstractHealthDisplayStrategy : MonoBehaviour
{
    /// <summary>
    /// Abstract function of DisplayText. Will be different for each concrete wave displaying strategy
    /// </summary>
    public abstract void DisplayText(string pText);
}
