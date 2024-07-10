using UnityEngine;

/// <summary>
/// Abstract class for shop time calculation, see basic shop time model strategy class for concrete implementation
/// </summary>
public abstract class AbstractShopTimeModelStrategy : MonoBehaviour
{
    [SerializeField]
    protected string shopTimeString; //GUI time text for during shop time
    [SerializeField]
    protected string shopTimeStringAlt; //GUI time text for during wave
    [SerializeField]
    protected int shopTimePerShopRound; //Time per shop round
    [SerializeField]
    protected GameObject skipShopTimeButton; //Button that skips shop time

    protected AbstractShopTimeDisplayStrategy shopTimeDisplayStrategy; //Strategy for displaying shop time in GUI
    protected int currentShopTime; //Current shop time

    /// <summary>
    /// Initializes fields, updates time GUI
    /// </summary>
    public void Initialize(AbstractShopTimeDisplayStrategy pTimeDisplayer)
    {
        shopTimeDisplayStrategy = pTimeDisplayer;
        currentShopTime = shopTimePerShopRound + 1;
        UpdateShopTime();
    }

    /// <summary>
    /// Returns the skip shop time button
    /// </summary>
    public GameObject GetSkipShopTimeButton()
    {
        return skipShopTimeButton;
    }

    /// <summary>
    /// Abstract function of UpdateShopTime. Will be different for each concrete time model strategy
    /// </summary>
    public abstract bool UpdateShopTime(bool pSkipShopTime = false);
}
