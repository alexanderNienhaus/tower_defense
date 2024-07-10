using UnityEngine;

/// <summary>
/// Controls the actions that are executed when a shop action is valid. Listens to the enough money event. At this point the shop action is
/// valid and will be executed.
/// When selling a tower, it gets destroyed.
/// When buying a tower, the new one is instantiated.
/// When upgrading a tower, the old standard tower gets destroyed and the a new upgraded tower is instantiated in its place.
/// This class also calls back to the beginning of the shop workflow and adds or removes the towers from the towers owned list of 
/// the shop open controller
/// </summary>
public class ShopDoActionController : MonoBehaviour
{
    [SerializeField]
    private TowerChangedEvent towerChangedEvent;

    /// <summary>
    /// Listens to the enough money event and executes the desired shop action by either instantiating a new tower, destroying an old one or both
    /// </summary>
    public void OnEnoughMoney(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        switch (pShopAction)
        {
            case ShopAction.Buy:
                TowerController towerControllerBought = CreateNewTower(pPosition, pTower);
                towerChangedEvent.Raise(pPosition, towerControllerBought, pShopAction);
                break;
            case ShopAction.Upgrade:
                TowerController towerControllerUpgraded = CreateNewTower(pPosition, pTower);
                towerChangedEvent.Raise(pPosition, towerControllerUpgraded, pShopAction);

                break;
            case ShopAction.Sell:
                towerChangedEvent.Raise(pPosition, pTower, pShopAction);
                break;
        }
    }

    /// <summary>
    /// Create a new tower by instantiating its prefab
    /// </summary>
    private TowerController CreateNewTower(Vector3 pPosition, TowerController pTower)
    {
        TowerController towerController = Instantiate(pTower, pPosition, Quaternion.identity);
        return towerController;
    }
}
