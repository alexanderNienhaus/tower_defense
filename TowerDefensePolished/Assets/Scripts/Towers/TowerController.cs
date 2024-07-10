using UnityEngine;

/// <summary>
/// Controls the towers, specifically the tower model and the attacker, see those classes for implementation detail
/// </summary>
public class TowerController : MonoBehaviour
{
    [SerializeField]
    private AbstractTowerModelStrategy towerModelStrategy; //Tower model strategy. Base values that are independent from attack are defined here
    [SerializeField]
    private AbstractAttackStrategy attackStrategy; //Attack strategy. Defines attack

    private AbstractTowerModelStrategy hiddenAbstractTowerModelStrategyChangeCheck; //Hidden field for scriptabe object, if SO is changed at runtime, instantiate it again
    private AbstractAttackStrategy hiddenAbstractAttackStrategyChangeCheck; //Hidden field for scriptabe object, if SO is changed at runtime, instantiate it again

    /// <summary>
    /// Returns tower model strategy
    /// </summary>
    public AbstractTowerModelStrategy GetTowerModelStrategy()
    {
        return towerModelStrategy;
    }

    /// <summary>
    /// Returns attack strategy
    /// </summary>
    public AbstractAttackStrategy GetAttackStrategy()
    {
        return attackStrategy;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        attackStrategy.OnStart();
    }

    /// <summary>
    /// Initializes strategies by instantiating a copy of them. This is necessary because multiple towers all need their own attacker and model
    /// but SOs are stored as reference
    /// </summary>
    private void Initialize()
    {
        InitializeTowerModel(towerModelStrategy);
        InitializeAttacker(attackStrategy);
    }

    /// <summary>
    /// Initializes the path finding model by instantiating a copy of its SO. Starts or stops the nav mesh agent accordingly
    /// </summary>
    private void InitializeTowerModel(AbstractTowerModelStrategy pAbstractTowerModelStrategy)
    {
        towerModelStrategy = Instantiate(pAbstractTowerModelStrategy);
        hiddenAbstractTowerModelStrategyChangeCheck = towerModelStrategy;
    }

    /// <summary>
    /// Initializes the path finding model by instantiating a copy of its SO. Starts or stops the nav mesh agent accordingly
    /// </summary>
    private void InitializeAttacker(AbstractAttackStrategy pAbstractAttackStrategy)
    {
        attackStrategy = Instantiate(pAbstractAttackStrategy);
        attackStrategy.Initialize(gameObject, transform);
        hiddenAbstractAttackStrategyChangeCheck = attackStrategy;
    }

    /// <summary>
    /// Calls the on fixed update function of the attack strategy
    /// </summary>
    private void FixedUpdate()
    {
        attackStrategy.OnFixedUpdate();
    }

    /// <summary>
    /// Calls the on update function of the attack strategy, and checks for SO changes
    /// </summary>
    private void Update()
    {
        attackStrategy.OnUpdate();
        CheckForScriptabeObjectChange();
    }

    /// <summary>
    /// Checks if SO is changed at runtime and initializes it again if so
    /// </summary>
    private void CheckForScriptabeObjectChange()
    {
        if (towerModelStrategy != hiddenAbstractTowerModelStrategyChangeCheck)
        {
            InitializeTowerModel(towerModelStrategy);
        }
        if (attackStrategy != hiddenAbstractAttackStrategyChangeCheck)
        {
            InitializeAttacker(attackStrategy);
        }
    }

    private void OnDestroy()
    {
        Destroy(attackStrategy);
        Destroy(towerModelStrategy);
    }
}
