using UnityEngine;

/// <summary>
/// Abstract class for health calculating, see basic health model strategy class for concrete implementation
/// </summary>
public abstract class AbstractHealthModelStrategy : MonoBehaviour
{
    [SerializeField]
    private string healthString; //GUI health text
    [SerializeField]
    private int startHealth; //Amount of start health
    [SerializeField]
    private ToggleInvincibleBaseSingleton toggleInvincibleBaseSingleton; //Singleton for debug option to toggle invinible base

    protected AbstractHealthDisplayStrategy healthDisplayStrategy;  //Strategy for displaying health in GUI
    protected int currentHealth;  //Current amount of health

    /// <summary>
    /// Initializes fields, updates health GUI
    /// </summary>
    public void Initialize(AbstractHealthDisplayStrategy pHealthDisplayer)
    {
        healthDisplayStrategy = pHealthDisplayer;
        currentHealth = startHealth;
        UpdateHealth();
    }

    /// <summary>
    /// Calculates current health amount and displays it in GUI
    /// </summary>
    public void UpdateHealth(int pDamage = 0)
    {
        if(pDamage != 0)
            CalculateCurrentHealth(pDamage);
    }

    /// <summary>
    /// Returns current health field
    /// </summary>
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    protected virtual void Update()
    {
        DisplayCurrentHealth();
    }

    /// <summary>
    /// Displays the current amount of health
    /// </summary>
    protected void DisplayCurrentHealth()
    {
        if (toggleInvincibleBaseSingleton != null && toggleInvincibleBaseSingleton.GetInvincibleBase())
        {
            healthDisplayStrategy.DisplayText(healthString + toggleInvincibleBaseSingleton.GetInfiniteHealthString());
        }
        else
        {
            healthDisplayStrategy.DisplayText(healthString + currentHealth);
        }
    }

    /// <summary>
    /// Abstract function of CalculateCurrentHealth. Will be different for each concrete health model strategy
    /// </summary>
    protected abstract void CalculateCurrentHealth(int pDamage);
}
