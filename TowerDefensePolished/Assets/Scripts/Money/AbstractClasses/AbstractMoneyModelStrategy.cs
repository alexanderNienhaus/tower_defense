using UnityEngine;

/// <summary>
/// Abstract class for money calculation, see basic money model strategy class for concrete implementation
/// </summary>
public abstract class AbstractMoneyModelStrategy : MonoBehaviour
{
    [SerializeField]
    private string moneyString;  //GUI money text
    [SerializeField]
    private int startMoney; //Start amount of money
    [SerializeField]
    ToggleInfiniteMoneySingleton toggleInfiniteMoneySingleton; //Singleton for debug setting of toggling infinite money

    protected AbstractMoneyDisplayStrategy moneyDisplayStrategy; //Strategy for displaying money
    protected int currentMoney; //Current amount of money

    /// <summary>
    /// Initializes fields, updates wave GUI
    /// </summary>
    public void Initialize(AbstractMoneyDisplayStrategy pMoneyDisplayer)
    {
        moneyDisplayStrategy = pMoneyDisplayer;
        currentMoney = startMoney;
        UpdateMoney();
    }

    /// <summary>
    /// Calculates new money amount and displays it in GUI
    /// </summary>
    public void UpdateMoney(int pAddition = 0)
    {
        if (pAddition != 0)
            CalculateCurrentMoney(pAddition);
    }

    /// <summary>
    /// Returns current money field
    /// </summary>
    public int GetCurrentMoney()
    {
        return currentMoney;
    }

    protected virtual void Update()
    {
        DisplayCurrentMoney();
    }

    /// <summary>
    /// Displays the current amount of money
    /// </summary>
    protected void DisplayCurrentMoney()
    {
        if (toggleInfiniteMoneySingleton != null && toggleInfiniteMoneySingleton.GetInfiniteMoney())
        {
            moneyDisplayStrategy.DisplayText(moneyString + toggleInfiniteMoneySingleton.GetInfiniteMoneyString());
        }
        else
        {
            moneyDisplayStrategy.DisplayText(moneyString + currentMoney);
        }
    }

    /// <summary>
    /// Abstract function CalculateCurrentMoney. Will be different for each concrete money model strategy
    /// </summary>
    protected abstract void CalculateCurrentMoney(int pAddition);
}
