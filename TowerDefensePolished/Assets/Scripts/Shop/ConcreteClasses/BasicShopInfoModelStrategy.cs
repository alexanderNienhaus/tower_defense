using UnityEngine;

/// <summary>
/// Concrete class for creation of shop info elements. Gets called by the shop info controller and formats strings with the correct data
/// for the correct messages.
/// For the not right time event, the message is just a string.
/// For the not enough money and enough money event, the messages contain the shop action and the tower name as well as an information about
/// wehter or not the action was successfull.
/// The class then calls the display method of the shop info displaying strategy
/// </summary>
public class BasicShopInfoModelStrategy : AbstractShopInfoModelStrategy
{
    [SerializeField]
    private float timeToLive; //Time till the shop info disappears

    /// <summary>
    /// Displays the shop info when a shop action was sucessfully executed
    /// </summary>
    public override void GetEnoughMoneyInfoString(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        TowerLevel towerLevel = pTower.GetTowerModelStrategy().GetTowerLevel();
        if (pShopAction == ShopAction.Upgrade)
            towerLevel = TowerLevel.Standard;
        string str = string.Format(enoughMoneyString, GetShopActionString(pShopAction, false), GetTowerLevelString(towerLevel), GetTowerTypeString(pTower.GetTowerModelStrategy().GetTowerType()));
        abstractShopInfoDisplayStrategy.DisplayText(str, timeToLive);
    }

    /// <summary>
    /// Displays the shop info when there is not enough money
    /// </summary>
    public override void GetNotEnoughMoneyInfoString(ShopAction pShopAction, TowerType pTowerType, TowerLevel pTowerLevel)
    {
        if (pShopAction == ShopAction.Upgrade)
            pTowerLevel = TowerLevel.Standard;
        string str = string.Format(notEnoughMoneyString, GetShopActionString(pShopAction, true), GetTowerLevelString(pTowerLevel), GetTowerTypeString(pTowerType));
        abstractShopInfoDisplayStrategy.DisplayText(str, timeToLive);
    }

    /// <summary>
    /// Displays the shop info when the shop was accessed at the wrong time
    /// </summary>
    public override void GetNotRightTimeInfoString()
    {
        abstractShopInfoDisplayStrategy.DisplayText(notRightTimeString, timeToLive);
    }


    /// <summary>
    /// Return the shop action as a string
    /// </summary>
    private string GetShopActionString(ShopAction pShopAction, bool pSimplePresence)
    {
        string str = "";
        switch (pShopAction)
        {
            case ShopAction.Buy:
                if (pSimplePresence)
                {
                    str = "buy";
                }
                else
                {
                    str = "bought";
                }
                break;
            case ShopAction.Upgrade:
                if (pSimplePresence)
                {
                    str = "upgrade";
                }
                else
                {
                    str = "upgraded";
                }
                break;
            case ShopAction.Sell:
                if (pSimplePresence)
                {
                    str = "sell";
                }
                else
                {
                    str = "sold";
                }
                break;
        }
        return str;
    }

    /// <summary>
    /// Return the tower type as a string
    /// </summary>
    private string GetTowerTypeString(TowerType pTowerType)
    {
        string str = "";
        switch (pTowerType)
        {
            case TowerType.Attack:
                str = "Single-Attack-Tower";
                break;
            case TowerType.Aoe:
                str = "Aoe-Tower";
                break;
            case TowerType.Debuff:
                str = "Debuff-Tower";
                break;
        }
        return str;
    }

    /// <summary>
    /// Return the tower level as a string
    /// </summary>
    private string GetTowerLevelString(TowerLevel pTowerLevel)
    {
        string str = "";
        switch (pTowerLevel)
        {
            case TowerLevel.Standard:
                str = "Standard";
                break;
            case TowerLevel.Upgraded:
                str = "Upgraded";
                break;
        }
        return str;
    }
}
