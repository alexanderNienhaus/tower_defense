using UnityEngine;

/// <summary>
/// Abstract class for creating shop info elements, see basic shop info model strategy class for concrete implementation
/// </summary>
public abstract class AbstractShopInfoModelStrategy : MonoBehaviour
{
    [SerializeField]
    protected string enoughMoneyString; //String for message when shop action is valid
    [SerializeField]
    protected string notEnoughMoneyString; //String for not enough money
    [SerializeField]
    protected string notRightTimeString; //String for not the right time

    protected AbstractShopInfoDisplayStrategy abstractShopInfoDisplayStrategy; //Strategy for displaying shop info

    /// <summary>
    /// Initialize strategy, update GUI
    /// </summary>
    public void Initialize(AbstractShopInfoDisplayStrategy pAbstractShopInfoDisplayer)
    {
        abstractShopInfoDisplayStrategy = pAbstractShopInfoDisplayer;
    }

    /// <summary>
    /// Abstract function of DisplayEnoughMoneyInfo. Will be different for each concrete shop info model strategy
    /// </summary>
    public abstract void GetEnoughMoneyInfoString(Vector3 pPosition, TowerController pTower, ShopAction pShopAction);

    /// <summary>
    /// Abstract function of DisplayNotEnoughMoneyInfo. Will be different for each concrete shop info model strategy
    /// </summary>
    public abstract void GetNotEnoughMoneyInfoString(ShopAction pShopAction, TowerType pTowerType, TowerLevel pTowerLevel);

    /// <summary>
    /// Abstract function of DisplayNotRightTimeInfo. Will be different for each concrete shop info model strategy
    /// </summary>
    public abstract void GetNotRightTimeInfoString();
}
