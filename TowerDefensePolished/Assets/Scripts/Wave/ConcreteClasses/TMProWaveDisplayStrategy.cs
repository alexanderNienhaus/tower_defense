using UnityEngine;
using TMPro;

/// <summary>
/// Concrete class for wave displaying, uses TMPro
/// </summary>
public class TMProWaveDisplayStrategy : AbstractWaveDisplayStrategy
{
    [SerializeField]
    private TextMeshProUGUI tmProGuiWaveText; //TMPro wave displaying element

    /// <summary>
    /// Concrete implementation of DisplayText. Displays string in TMPro element
    /// </summary>
    public override void DisplayText(string pText)
    {
        tmProGuiWaveText.SetText(pText);
    }
}
