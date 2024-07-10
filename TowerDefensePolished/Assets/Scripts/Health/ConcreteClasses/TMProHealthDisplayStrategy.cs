using UnityEngine;
using TMPro;

/// <summary>
/// Concrete class for health displaying, uses TMPro
/// </summary>
public class TMProHealthDisplayStrategy : AbstractHealthDisplayStrategy
{
    [SerializeField]
    private TextMeshProUGUI tmProGuiHealthText; //TMPro health displaying element

    /// <summary>
    /// Concrete implementation of DisplayText. Displays string in TMPro element
    /// </summary>
    public override void DisplayText(string pText)
    {
        tmProGuiHealthText.SetText(pText);
    }
}
