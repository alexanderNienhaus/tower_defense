using UnityEngine;

/// <summary>
/// Controls the money, specifically the money model and the money displayer. Passes events onto them and raises events itself.
/// Listens to the shop option clicked event and checks if the player has enough money for the shop actions, then eiter raises
/// the enough money or not enough money event. The class also calls the update money function of its money model strategy whenever
/// an action is valid, to either add money (tower was sold) or subtract money (tower was bought or upgraded).
/// Also listens to the enemy dies event and adds the carried money of the enemy
/// Also holds the infinite money singleton, to instantly greenlight all shop actions if the debugging option is toggled
/// </summary>
public class MoneyController : MonoBehaviour
{
    [SerializeField]
    NotEnoughMoneyEvent notEnoughMoneyEvent; //Event for not enough money for shop action
    [SerializeField]
    EnoughMoneyEvent enoughMoneyEvent; //Event when shop action is valid
    [SerializeField]
    ToggleInfiniteMoneySingleton toggleInfiniteMoneySingleton; //Singleton for debug option to toggle infinite money

    private AbstractMoneyModelStrategy moneyModelStrategy; //Strategy for calculating money


    /// <summary>
    /// Listens to the enemy dies event, updates money accordingly
    /// </summary>
    public void OnEnemyDies(int pCarriedMoney, EnemyController pEnemyController)
    {
        moneyModelStrategy.UpdateMoney(pCarriedMoney);
    }

    /// <summary>
    /// Listens to the shop option clicked event, depending on the current amount of money, raises either the enough or not enough money event
    /// </summary>
    public void OnShopOptionClicked(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        int currentMoney = moneyModelStrategy.GetCurrentMoney();
        bool infiniteMoney = toggleInfiniteMoneySingleton != null ? toggleInfiniteMoneySingleton.GetInfiniteMoney() : false;
        AbstractTowerModelStrategy towerModelStrategy = pTower.GetTowerModelStrategy();

        switch (pShopAction)
        {
            case ShopAction.Upgrade:
            case ShopAction.Buy:
                int buyCost = towerModelStrategy.GetBuyCost();
                if (currentMoney >= buyCost || infiniteMoney)
                {
                    if (!infiniteMoney)
                        moneyModelStrategy.UpdateMoney(-buyCost);
                    enoughMoneyEvent.Raise(pPosition, pTower, pShopAction);
                }
                else
                {
                    notEnoughMoneyEvent.Raise(pShopAction, towerModelStrategy.GetTowerType(), towerModelStrategy.GetTowerLevel());
                }
                break;
            case ShopAction.Sell:
                moneyModelStrategy.UpdateMoney(towerModelStrategy.GetSellValue());
                enoughMoneyEvent.Raise(pPosition, pTower, pShopAction);
                break;
        }
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
        AbstractMoneyDisplayStrategy moneyDisplayStrategy = GetComponent<AbstractMoneyDisplayStrategy>();
        if (moneyDisplayStrategy != null)
        {
            moneyModelStrategy = GetComponent<AbstractMoneyModelStrategy>();
            if (moneyModelStrategy != null)
            {
                moneyModelStrategy.Initialize(moneyDisplayStrategy);
            }
            else
            {
                throw new System.Exception("There is no component that implements the AbstractMoneyModel abstract class.");
            }
        }
        else
        {
            throw new System.Exception("There is no component that implements the AbstractMoneyDisplayer abstract class.");
        }
    }
}
