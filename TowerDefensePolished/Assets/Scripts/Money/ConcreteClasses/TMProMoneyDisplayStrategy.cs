using UnityEngine;
using TMPro;

/// <summary>
/// Concrete class for money displaying, uses TMPro
/// </summary>
public class TMProMoneyDisplayStrategy : AbstractMoneyDisplayStrategy
{
    [SerializeField]
    private TextMeshProUGUI tmProGuiMoneyText; //TMPro wave displaying element

    /// <summary>
    /// Concrete implementation of DisplayText. Displays string in TMPro element
    /// </summary>
    public override void DisplayText(string pText)
    {
        tmProGuiMoneyText.SetText(pText);
    }
}
