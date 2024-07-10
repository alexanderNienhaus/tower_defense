using UnityEngine;

/// <summary>
/// Enum for different shop actions
/// </summary>
public enum ShopAction
{
    Buy,
    Upgrade,
    Sell
}

/// <summary>
/// Controls the shop options, specifically the shop options model strategy and the shop options display strategy.
/// Passes events onto them and raises events itself
/// This controller listens to the right time event which means that the player opnened the shop at the correct time.
/// The event contains a tower if the player clicked on an existing tower to upgrade or sell it, or null if the tower spot was empty,
/// to buy a new tower. The class passes the event data to the shop options model strategy which then creates the correct shop elements
/// and call the shop options display strategy to display them.
/// The class also calls the shop open clicked event if one of the shop options is selected by the player
/// </summary>
public class ShopOptionsController : MonoBehaviour
{
    [SerializeField]
    private ShopOptionClickedEvent shopOptionClickedEvent; //Event for wehen a shop option is clicked

    private AbstractShopOptionsDisplayStrategy shopOptionsDisplayStrategy; //Strategy for displaying the shop options in GUI
    private AbstractShopOptionsModelStrategy shopOptionsModelStrategy; //Strategy for creating the shop option

    /// <summary>
    /// Listens to the on right time event and shows the specific shop options
    /// </summary>
    public void OnRightTime(Vector3 pPosition, TowerController pTower)
    {
        shopOptionsModelStrategy.ShowShopOptions(pPosition, pTower);
    }

    /// <summary>
    /// Listens to the on shop time over event and hides all shop options
    /// </summary>
    public void OnShopTimeOver()
    {
        shopOptionsDisplayStrategy.HideAllShopOptions();
    }

    /// <summary>
    /// Raise the event for shop option clicked and pass it the correct prefab, hide all shop options
    /// </summary>
    public void BtnClickedBuyAoeTower()
    {
        shopOptionsDisplayStrategy.HideAllShopOptions();
        shopOptionClickedEvent.Raise(shopOptionsModelStrategy.GetTowerPosition(),
            shopOptionsModelStrategy.GetCorrectTowerPrefab(ShopAction.Buy, TowerType.Aoe), ShopAction.Buy);
    }

    /// <summary>
    /// Raise the event for shop option clicked and pass it the correct prefab, hide all shop options
    /// </summary>
    public void BtnClickedBuyAttackTower()
    {
        shopOptionsDisplayStrategy.HideAllShopOptions();
        shopOptionClickedEvent.Raise(shopOptionsModelStrategy.GetTowerPosition(),
            shopOptionsModelStrategy.GetCorrectTowerPrefab(ShopAction.Buy, TowerType.Attack), ShopAction.Buy);
    }

    /// <summary>
    /// Raise the event for shop option clicked and pass it the correct prefab, hide all shop options
    /// </summary>
    public void BtnClickedBuyDebuffTower()
    {
        shopOptionsDisplayStrategy.HideAllShopOptions();
        shopOptionClickedEvent.Raise(shopOptionsModelStrategy.GetTowerPosition(),
            shopOptionsModelStrategy.GetCorrectTowerPrefab(ShopAction.Buy, TowerType.Debuff), ShopAction.Buy);
    }

    /// <summary>
    /// Raise the event for shop option clicked and pass it the correct prefab, hide all shop options
    /// </summary>
    public void BtnClickedUpgradeTower()
    {
        shopOptionsDisplayStrategy.HideAllShopOptions();
        shopOptionClickedEvent.Raise(shopOptionsModelStrategy.GetTowerPosition(),
            shopOptionsModelStrategy.GetCorrectTowerPrefab(ShopAction.Upgrade), ShopAction.Upgrade);
    }

    /// <summary>
    /// Raise the event for shop option clicked and pass it the correct prefab, hide all shop options
    /// </summary>
    public void BtnClickedSellTower()
    {
        shopOptionsDisplayStrategy.HideAllShopOptions();
        shopOptionClickedEvent.Raise(shopOptionsModelStrategy.GetTowerPosition(),
            shopOptionsModelStrategy.GetCorrectTowerPrefab(ShopAction.Sell), ShopAction.Sell);
    }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Gets components, initializes strategy
    /// </summary>
    private void Initialize()
    {
        shopOptionsDisplayStrategy = GetComponent<AbstractShopOptionsDisplayStrategy>();
        if (shopOptionsDisplayStrategy != null)
        {
            shopOptionsModelStrategy = GetComponent<AbstractShopOptionsModelStrategy>();
            if (shopOptionsModelStrategy != null)
            {
                shopOptionsModelStrategy.Initialize(shopOptionsDisplayStrategy);
            }
            else
            {
                throw new System.Exception("There is no component that implements the AbstractShopOptionsModel abstract class.");
            }
        }
        else
        {
            throw new System.Exception("There is no component that implements the AbstractShopOptionsDisplayer abstract class.");
        }
    }
}
