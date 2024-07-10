using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the waves, specifically the wave model and the wave displayer. Passes events onto them and raises events itself.
/// This controller operates a wave by calling the do spawning method of its wave model in an interval specified by the wave model.
/// The start of a wave is triggered by listening to the shop time over event.
/// It also uses the alive enemies singleton to keep track of all enemies alive an pushes a wave over event if the last one is dead.
/// If that happens during the last wave, it pushes a game won event instead.
/// </summary>
public class WaveController : MonoBehaviour
{
    [SerializeField]
    private WaveOverEvent waveOverEvent; //Event for end of wave
    [SerializeField]
    private GameWonEvent gameWonEvent; //Event for game won
    [SerializeField]
    private AliveEnemiesSingleton aliveEnemiesSingleton; //Scriptable object singleton that holds a list of all currently alive enemies

    private AbstractWaveModelStrategy waveModelStrategy; //Strategy for spawning waves
    private bool spawnerActive; //Bool or activity of spawner
    private bool enemiesActive; //Bool for existance of enemies

    /// <summary>
    /// Listens to night arived event, starts wave
    /// </summary>
    public void OnNightArived()
    {
        StartCoroutine(SpawnWaiter());
    }

    /// <summary>
    /// Starts one wave, updates GUI
    /// </summary>
    public void StartSpawning()
    {
        waveModelStrategy.UpdateWaveCount(1);
        spawnerActive = true;
        enemiesActive = true;
        waveModelStrategy.InitializeWave();
        StartCoroutine(SpawnCoroutine());
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
        AbstractWaveDisplayStrategy waveDisplayStrategy = GetComponent<AbstractWaveDisplayStrategy>();
        if (waveDisplayStrategy != null)
        {
            waveModelStrategy = GetComponent<AbstractWaveModelStrategy>();
            if (waveModelStrategy != null)
            {
                waveModelStrategy.Initialize(waveDisplayStrategy);
            }
            else
            {
                throw new System.Exception("There is no component that implements the AbstractWaveModel abstract class.");
            }
        }
        else
        {
            throw new System.Exception("There is no component that implements the AbstractWaveDisplayer abstract class.");
        }
    }

    private void Update()
    {
        UpdateWave();
    }

    /// <summary>
    /// Keeps wave running, checks for end, raises events for wave over and game end
    /// </summary>
    private void UpdateWave()
    {
        if (aliveEnemiesSingleton != null && aliveEnemiesSingleton.GetAliveEnemies().Count <= 0 && !spawnerActive && enemiesActive)
        {
            enemiesActive = false;
            if (waveModelStrategy.GetCurrentWave() == waveModelStrategy.GetTotalNumberOfWaves())
            {
                StartCoroutine(GameEndWaiter());
            }
            else
            {
                waveOverEvent.Raise();
            }
        }
    }

    /// <summary>
    /// Keeps the enemy spawning coroutine active
    /// </summary>
    private IEnumerator SpawnCoroutine()
    {
        while (spawnerActive)
        {
            spawnerActive = waveModelStrategy.DoSpawning();
            yield return new WaitForSeconds(waveModelStrategy.GetDelayBetweenEnemies());
        }
    }

    private IEnumerator SpawnWaiter()
    {
        yield return new WaitForSeconds(waveModelStrategy.GetInitialSpawnDelay());
        StartSpawning();
    }

    private IEnumerator GameEndWaiter()
    {
        yield return new WaitForSeconds(waveModelStrategy.GetInitialSpawnDelay());
        gameWonEvent.Raise();
    }
}
