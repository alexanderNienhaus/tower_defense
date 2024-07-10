using UnityEngine;
using TMPro;

/// <summary>
/// Concrete class for time displaying, uses TMPro
/// </summary>
public class TMProShopTimeDisplayStrategy : AbstractShopTimeDisplayStrategy
{
    [SerializeField]
    private TextMeshProUGUI tmProGuiShopTimeText;

    /// <summary>
    /// Concrete implementation of DisplayText. Displays string in TMPro element
    /// </summary>
    public override void DisplayText(string pText)
    {
        tmProGuiShopTimeText.SetText(pText);
    }
}
