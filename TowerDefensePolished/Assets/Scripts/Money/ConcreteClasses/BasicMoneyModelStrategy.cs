/// <summary>
/// Concrete class for money calculation
/// </summary>
public class BasicMoneyModelStrategy : AbstractMoneyModelStrategy
{
    /// <summary>
    /// Calculates current amount of money
    /// </summary>
    protected override void CalculateCurrentMoney(int pAddition)
    {
        currentMoney += pAddition;
    }
}
