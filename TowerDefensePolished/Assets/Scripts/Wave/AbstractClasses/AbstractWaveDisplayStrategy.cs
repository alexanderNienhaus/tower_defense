using UnityEngine;

/// <summary>
/// Abstract class for wave displaying, see tmpro wave displaying strategy class for concrete implementation
/// </summary>
public abstract class AbstractWaveDisplayStrategy : MonoBehaviour
{
    /// <summary>
    /// Abstract function of DisplayText. Will be different for each concrete wave displaying strategy
    /// </summary>
    public abstract void DisplayText(string pText);
}
