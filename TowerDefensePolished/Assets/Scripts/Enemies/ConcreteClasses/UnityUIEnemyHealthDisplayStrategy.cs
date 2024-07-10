using UnityEngine.UI;

/// <summary>
/// Concrete strategy for displaying the enemy health, uses the unity UI to create a healthbar
/// </summary>
public class UnityUIEnemyHealthDisplayStrategy : AbstractEnemyHealthDisplayStrategy
{
    private Slider slider; //Slider of health bar

    /// <summary>
    /// Concrete implementation of SetHealth function, sets the value of the health bar
    /// </summary>
    public override void SetHealth(int pCurrentHealth, int maxHp)
    {
        float newHealth = (float)pCurrentHealth / (float)maxHp * 100.0f;
        if(slider != null)
            slider.value = (int)newHealth;
    }

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Gets component
    /// </summary>
    private void Initialize()
    {
        slider = transform.GetComponentInChildren<Slider>();
        if (slider == null)
        {
            throw new System.Exception("There is no Slider component in children.");
        }
    }
}
