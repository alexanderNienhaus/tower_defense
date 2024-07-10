using UnityEngine;
using TMPro;

/// <summary>
/// Concrete class for shop info displaying, uses TMPro. Destroys the text after a specified time to live
/// </summary>
public class TMProShopInfoDisplayStrategy : AbstractShopInfoDisplayStrategy
{
    [SerializeField]
    private TextMeshProUGUI tmProGuiShopInfoText; //TMPro wave displaying element

    private float totalTimeToLive; //Total time after which a GUI element is destroyed
    private float currentTime; //Time counter

    /// <summary>
    /// Concrete implementation of DisplayText. Displays string in TMPro element
    /// </summary>
    public override void DisplayText(string pText, float pTotalTimeToLive)
    {
        totalTimeToLive = pTotalTimeToLive;
        currentTime = 0;
        tmProGuiShopInfoText.SetText(pText);
    }

    /// <summary>
    /// Counts the time that the shop info has been displayed and destroys it after a defined duration
    /// </summary>
    private void HideShopInfo()
    {
        if (currentTime < totalTimeToLive)
        {
            currentTime += Time.fixedDeltaTime;
        }

        if (currentTime >= totalTimeToLive)
        {
            tmProGuiShopInfoText.SetText("");
        }
    }
    private void FixedUpdate()
    {
        HideShopInfo();
    }
}
