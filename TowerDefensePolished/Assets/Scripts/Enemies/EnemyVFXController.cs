using UnityEngine;

/// <summary>
/// Controls the VFX of the enemy, specifically the enemy dies vfx model strategy. Passes events onto it
/// </summary>
public class EnemyVFXController : MonoBehaviour
{
    private AbstractEnemyDiesVFXModelStrategy enemyDiesVFXModelStrategy; //Strategy for displaying the enemy dies vfx

    /// <summary>
    /// Listens to the enemy dies event and displays the enemy dies vfx
    /// </summary>
    public void OnEnemyDies(int pCarriedMoney, EnemyController pEnemyController)
    {
        enemyDiesVFXModelStrategy.CreateEnemyDiesVFX(pCarriedMoney, pEnemyController.transform.position);
    }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Initializes strategy
    /// </summary>
    private void Initialize()
    {
        enemyDiesVFXModelStrategy = GetComponent<AbstractEnemyDiesVFXModelStrategy>();
        if (enemyDiesVFXModelStrategy == null)
        {
            throw new System.Exception("There is no component that implements the AbstractEnemyDiesVFXDisplayStrategy abstract class.");
        }
    }
}
