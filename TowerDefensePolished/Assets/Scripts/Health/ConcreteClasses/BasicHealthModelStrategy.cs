/// <summary>
/// Concrete class for health calculation
/// </summary>
public class BasicHealthModelStrategy : AbstractHealthModelStrategy
{
    /// <summary>
    /// Calculates health
    /// </summary>
    protected override void CalculateCurrentHealth(int pDamage)
    {
        currentHealth -= pDamage;
    }
}
