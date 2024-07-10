using UnityEngine;

/// <summary>
/// Controls the information that is displayed after a shop action, specifically the shop info model and the shop info displayer.
/// Passes events onto them and raises events itself.
/// Listens to the not right time event (player tried to access the shop during a wave), to the not enough money event and the 
/// enough money event, calls the respective methods on the shop info model strategy and passes the event data onto them
/// </summary>
public class ShopInfoController : MonoBehaviour
{
    private AbstractShopInfoModelStrategy shopInfoModelStrategy; //Strategy for creating shop info

    /// <summary>
    /// Listens to the enough money event, displays the specific shop info
    /// </summary>
    public void OnEnoughMoney(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        shopInfoModelStrategy.GetEnoughMoneyInfoString(pPosition, pTower, pShopAction);
    }

    /// <summary>
    /// Listens to the not enough money event, displays the specific shop info
    /// </summary>
    public void OnNotEnoughMoney(ShopAction pShopAction, TowerType pTowerType, TowerLevel pTowerLevel)
    {
        shopInfoModelStrategy.GetNotEnoughMoneyInfoString(pShopAction, pTowerType, pTowerLevel);
    }

    /// <summary>
    /// Listens to the not right time event, displays the specific shop info
    /// </summary>
    public void OnNotRightTime()
    {
        shopInfoModelStrategy.GetNotRightTimeInfoString();
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
        AbstractShopInfoDisplayStrategy shopInfoDisplayStrategy = GetComponent<AbstractShopInfoDisplayStrategy>();
        if (shopInfoDisplayStrategy != null)
        {
            shopInfoModelStrategy = GetComponent<AbstractShopInfoModelStrategy>();
            if (shopInfoModelStrategy != null)
            {
                shopInfoModelStrategy.Initialize(shopInfoDisplayStrategy);
            }
            else
            {
                throw new System.Exception("There is no component that implements the AbstractShopInfoModel abstract class.");
            }
        }
        else
        {
            throw new System.Exception("There is no component that implements the AbstractShopInfoDisplayer abstract class.");
        }
    }
}
