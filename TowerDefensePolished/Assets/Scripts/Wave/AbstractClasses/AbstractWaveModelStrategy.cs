using UnityEngine;

/// <summary>
/// Abstract class for wave spawning, needs to overwritten. See Basic wave model strategy class for concrete implementation
/// </summary>
public abstract class AbstractWaveModelStrategy : MonoBehaviour
{
    [SerializeField]
    protected string waveText; //GUI wave text
    [SerializeField]
    protected int startWave; //Number of first wave
    [SerializeField]
    private float initialSpawnDelay; //Delay for spawning the first enemy

    AbstractWaveDisplayStrategy waveDisplayStrategy; //Strategy for displaying wave in GUI
    protected int currentWave; //Number of current wave
    protected float delayBetweenEnemies; //Delay between enemies spawning
    protected int totalNumberOfWaves; //Total number of waves

    /// <summary>
    /// Initializes fields, updates wave GUI
    /// </summary>
    public void Initialize(AbstractWaveDisplayStrategy pWaveDisplayer)
    {
        waveDisplayStrategy = pWaveDisplayer;
        currentWave = startWave;
        UpdateWaveCount();
    }

    /// <summary>
    /// Calculates new wave count and displays it in GUI
    /// </summary>
    public void UpdateWaveCount(int pStep = 0)
    {
        if (pStep != 0)
            CalculateWave(pStep);
        waveDisplayStrategy.DisplayText(waveText + currentWave);
    }

    /// <summary>
    /// Returns delay between enemies field
    /// </summary>
    public float GetDelayBetweenEnemies()
    {
        return delayBetweenEnemies;
    }

    /// <summary>
    /// Returns total number of waves field
    /// </summary>
    public float GetTotalNumberOfWaves()
    {
        return totalNumberOfWaves;
    }

    /// <summary>
    /// Returns current wave field
    /// </summary>
    public int GetCurrentWave()
    {
        return currentWave;
    }

    /// <summary>
    /// Returns initial spawn delay field
    /// </summary>
    public float GetInitialSpawnDelay()
    {
        return initialSpawnDelay;
    }

    /// <summary>
    /// Abstract function InitializeWave. Will be different for each concrete wave model strategy
    /// </summary>
    public abstract void InitializeWave();

    /// <summary>
    /// Abstract function of DoSpawning. Will be different for each concrete wave model strategy
    /// </summary>
    public abstract bool DoSpawning();

    /// <summary>
    /// Abstract function of CalculateWave. Will be different for each concrete wave model strategy
    /// </summary>
    protected abstract void CalculateWave(int pStep);
}
