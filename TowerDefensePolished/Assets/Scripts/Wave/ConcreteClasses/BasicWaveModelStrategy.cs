using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Concrete class for wave spawning. Has five waves with increasing difficulty and two different enemy types.
/// Every wave consists of a number of weak and strong enemies that are spawned in an interval, in a random order.
/// Enemies are instantiated from their prefabs, put at a spawn point and their target point for pathfinding is set.
/// This class also calls the update wave function of the wave displaying strategy to update the GUI
/// </summary>
public class BasicWaveModelStrategy : AbstractWaveModelStrategy
{
    [SerializeField]
    private GameObject spawnPoint; //Spawn point for enemies
    [SerializeField]
    private GameObject goal; //Goal for enemies
    [SerializeField]
    private EnemyController strongEnemyPrefab; //Prefab for strong enemies
    [SerializeField]
    private EnemyController weakEnemyPrefab; //Prefab for weak enemies
    [SerializeField]
    private int numberWeakEnemiesWaveOne; //Number of weak enmeis in wave one
    [SerializeField]
    private int numberStrongEnemiesWaveOne; //Number of strong enmeis in wave one
    [SerializeField]
    private float delayBetweenEnemiesWaveOne; //Delay between enemies in wave one
    [SerializeField]
    private int numberWeakEnemiesWaveTwo; //Number of weak enmeis in wave two
    [SerializeField]
    private int numberStrongEnemiesWaveTwo; //Number of strong enmeis in wave two
    [SerializeField]
    private float delayBetweenEnemiesWaveTwo; //Delay between enemies in wave two
    [SerializeField]
    private int numberWeakEnemiesWaveThree; //Number of weak enmeis in wave three
    [SerializeField]
    private int numberStrongEnemiesWaveThree; //Number of strong enmeis in wave three
    [SerializeField]
    private float delayBetweenEnemiesWaveThree; //Delay between enemies in wave three
    [SerializeField]
    private int numberWeakEnemiesWaveFour; //Number of weak enmeis in wave four
    [SerializeField]
    private int numberStrongEnemiesWaveFour; //Number of strong enmeis in wave four
    [SerializeField]
    private float delayBetweenEnemiesWaveFour; //Delay between enemies in wave four
    [SerializeField]
    private int numberWeakEnemiesWaveFive; //Number of weak enmeis in wave five
    [SerializeField]
    private int numberStrongEnemiesWaveFive; //Number of strong enmeis in wave five
    [SerializeField]
    private float delayBetweenEnemiesWaveFive; //Delay between enemies in wave five

    private List<int> enemiesOrder; //List used to spawn enemies in random order
    private int currentNumberStrongEnemies; //Number of strong enemies in current wave
    private int currentNumberWeakEnemies; //Number of weak enemies in current wave
    private int zFightingCounter;

    /// <summary>
    /// Concrete implementation of InitializeWave. Sets correct spawning values for each wave, shuffles order of enemies
    /// </summary>
    public override void InitializeWave()
    {
        switch (currentWave)
        {
            case 1:
                delayBetweenEnemies = delayBetweenEnemiesWaveOne;
                currentNumberStrongEnemies = numberStrongEnemiesWaveOne;
                currentNumberWeakEnemies = numberWeakEnemiesWaveOne;
                break;
            case 2:
                delayBetweenEnemies = delayBetweenEnemiesWaveTwo;
                currentNumberStrongEnemies = numberStrongEnemiesWaveTwo;
                currentNumberWeakEnemies = numberWeakEnemiesWaveTwo;
                break;
            case 3:
                delayBetweenEnemies = delayBetweenEnemiesWaveThree;
                currentNumberStrongEnemies = numberStrongEnemiesWaveThree;
                currentNumberWeakEnemies = numberWeakEnemiesWaveThree;
                break;
            case 4:
                delayBetweenEnemies = delayBetweenEnemiesWaveFour;
                currentNumberStrongEnemies = numberStrongEnemiesWaveFour;
                currentNumberWeakEnemies = numberWeakEnemiesWaveFour;
                break;
            case 5:
                delayBetweenEnemies = delayBetweenEnemiesWaveFive;
                currentNumberStrongEnemies = numberStrongEnemiesWaveFive;
                currentNumberWeakEnemies = numberWeakEnemiesWaveFive;
                break;
        }

        for (int i = 0; i < currentNumberWeakEnemies; i++)
        {
            enemiesOrder.Add(0);
        }
        for (int i = 0; i < currentNumberStrongEnemies; i++)
        {
            enemiesOrder.Add(1);
        }

        enemiesOrder = Shuffle(enemiesOrder);
        zFightingCounter = 0;
    }

    /// <summary>
    /// Concrete implementation of DoSpawning. Spawns either a strong or a weak enemy, based on random list
    /// </summary>
    public override bool DoSpawning()
    {
        if (enemiesOrder.Count > 0)
        {
            if (enemiesOrder[0] == 1)
            {
                SpawnStrongEnemy();
            }
            else
            {
                SpawnWeakEnemy();
            }
            enemiesOrder.RemoveAt(0);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Concrete implementation of CalculateWave. Calculates the current wave number
    /// </summary>
    protected override void CalculateWave(int pStep)
    {
        currentWave += pStep;
    }

    private List<int> Shuffle(List<int> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Set wave number, initialize list for order of enemies
    /// </summary>
    private void Initialize()
    {
        totalNumberOfWaves = 5;
        enemiesOrder = new List<int>();
    }

    /// <summary>
    /// Spawns a weak enemy by instatiating its prefab. Sets its position = spawn point and its target = goal
    /// </summary>
    private void SpawnWeakEnemy()
    {
        EnemyController weakEnemyController = Instantiate(weakEnemyPrefab);
        zFightingCounter--;
        SpriteRenderer sr = weakEnemyController.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder += zFightingCounter;
        }
        Vector3 position = spawnPoint.transform.position;
        weakEnemyController.transform.position = position;
        weakEnemyController.SetTarget(goal);
    }

    /// <summary>
    /// Spawns a strong enemy by instatiating its prefab. Sets its position = spawn point and its target = goal
    /// </summar
    private void SpawnStrongEnemy()
    {
        EnemyController strongEnemyController = Instantiate(strongEnemyPrefab);
        zFightingCounter--;
        SpriteRenderer sr = strongEnemyController.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder += zFightingCounter;
        }
        Vector3 position = spawnPoint.transform.position;
        strongEnemyController.transform.position = position;
        strongEnemyController.SetTarget(goal);
    }
}
