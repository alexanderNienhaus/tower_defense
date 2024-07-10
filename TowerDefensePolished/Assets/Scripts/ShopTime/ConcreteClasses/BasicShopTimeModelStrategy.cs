/// <summary>
/// Concrete class for shop time calculation
/// </summary>
public class BasicShopTimeModelStrategy : AbstractShopTimeModelStrategy
{
    /// <summary>
    /// Concrete implementation of UpdateShopTime. Coutns down to 0 and updates GUI accordingly
    /// </summary>
    public override bool UpdateShopTime(bool pSkipShopTime = false)
    {
        if (pSkipShopTime)
        {
            currentShopTime = 0;
        }

        if (currentShopTime <= 0)
        {
            currentShopTime = shopTimePerShopRound;
            shopTimeDisplayStrategy.DisplayText(shopTimeStringAlt);
            return false;
        }
        else
        {
            currentShopTime--;
            shopTimeDisplayStrategy.DisplayText(shopTimeString + currentShopTime);
            return true;
        }
    }
}
