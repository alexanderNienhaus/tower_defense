using UnityEngine;

/// <summary>
/// Controls health, specifically the health model and the health displayer. Passes events onto them and raises events itself.
/// Listens to the enemy reaches goal event and calls the update health function of its health model strategy.
/// It raises the game over event if the health reaches zero.
/// Also has the invincible base singleton and prevents damage if the debugging option is toggled.
/// </summary>
public class HealthController : MonoBehaviour
{
    [SerializeField]
    private GameLostEvent gameLostEvent; //Event for when game is lost
    [SerializeField]
    private ToggleInvincibleBaseSingleton toggleInvincibleBaseSingleton; //Singleton for debug option to toggle invinible base

    private AbstractHealthModelStrategy healthModelStrategy; //Strategy for calculating health

    /// <summary>
    /// Listens to enemy reaches goal event, updates health accordingly. If health reaches 0, raises the game lost event
    /// </summary>
    public void OnEnemyReachesGoal(int pDamage, EnemyController pEnemyController)
    {
        if (toggleInvincibleBaseSingleton != null && toggleInvincibleBaseSingleton.GetInvincibleBase())
            return;

        healthModelStrategy.UpdateHealth(pDamage);
        if (healthModelStrategy.GetCurrentHealth() <= 0)
        {
            gameLostEvent.Raise();
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
        AbstractHealthDisplayStrategy healthDisplayStrategy = GetComponent<AbstractHealthDisplayStrategy>();
        if (healthDisplayStrategy != null)
        {
            healthModelStrategy = GetComponent<AbstractHealthModelStrategy>();
            if (healthModelStrategy != null)
            {
                healthModelStrategy.Initialize(healthDisplayStrategy);
            }
            else
            {
                throw new System.Exception("There is no component that implements the AbstractHealthModel abstract class.");
            }
        }
        else
        {
            throw new System.Exception("There is component that implements the AbstractHealthDisplayer abstract class.");
        }
    }
}
