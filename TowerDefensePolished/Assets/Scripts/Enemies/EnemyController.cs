using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the enemies, specifically the enemies model and the pathfinder, see those classes for implementation detail.
/// Passes events onto them and raises events itself. Passes function calls onto the model strategy and retrieves fields out of it.
/// Raises the enemy dies event if its model strategies health value reaches zero.
/// Raises the enemy spawns event upon creation.
/// Raises the enemy reaches goal event if its collider touches the goal.
/// Its set target method is called by the wave model strategy whenever an enemy is spawned, to set the goal
/// Also calls the move funtion on its pathfinder strategy
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private AbstractEnemyModelStrategy enemyModelStrategy; //Enemy model strategy, defines the enemy
    [SerializeField]
    private AbstractPathFindingStrategy pathFindingStrategy; //Path finding strategy
    [SerializeField]
    private EnemySpawnsEvent enemySpawnsEvent; //Event when an enemy spawns
    [SerializeField]
    private EnemyReachesGoalEvent enemyReachesGoalEvent; //Event when an enemy reaches goal
    [SerializeField]
    private EnemyDiesEvent enemyDiesEvent; //Event when en enemy dies

    private GameObject target; //Target that the enemy wants to reach
    private CapsuleCollider2D capsuleCollider2D; //Capsule collider component
    private CircleCollider2D targetCircleCollider; //Circle collider component of target
    private NavMeshAgent navMeshAgent; //Navmesh agent component
    private AbstractEnemyModelStrategy hiddenAbstractEnemyModelStrategyChangeCheck; //Hidden field for scriptabe object, if SO is changed at runtime, instantiate it again
    private AbstractPathFindingStrategy hiddenAbstractPathFindingStrategyChangeCheck; //Hidden field for scriptabe object, if SO is changed at runtime, instantiate it again

    /// <summary>
    /// Does damage to the enemy
    /// </summary>
    public void DoDamage(int pDamage)
    {
        enemyModelStrategy.DoDamage(pDamage);
    }

    /// <summary>
    /// Slows the enemy
    /// </summary>
    public void DoSlow(float pSlowPercentage, float pSlowDuration)
    {
        enemyModelStrategy.DoSlow(pSlowPercentage, pSlowDuration);
    }

    /// <summary>
    /// Sets the target of the enemy
    /// </summary>
    public void SetTarget(GameObject pTarget)
    {
        target = pTarget;
    }

    /// <summary>
    /// Returns the velocity of the enemy
    /// </summary>
    public Vector3 GetVelocity()
    {
        return enemyModelStrategy.GetVelocity();
    }

    /// <summary>
    /// Returns the speed of the enemy
    /// </summary>
    public float GetSpeed()
    {
        return enemyModelStrategy.GetSpeed();
    }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Gets components, initializes strategies by instantiating a copy of the scriptable objects, raises enemy spawns event
    /// </summary>
    private void Initialize()
    {   
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if (capsuleCollider2D == null)
        {
            throw new System.Exception("There is no CapsuleCollider2D component.");
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            throw new System.Exception("There is no NavMeshAgent component.");
        }

        InitializePathfindingModel(pathFindingStrategy);
        InitializeEnemyModel(enemyModelStrategy);
        enemySpawnsEvent.Raise(this);
    }

    /// <summary>
    /// Initializes the enemy model by instantiating a copy of its SO
    /// </summary>
    private void InitializeEnemyModel(AbstractEnemyModelStrategy pAbstractEnemyModelStrategy)
    {
        AbstractEnemyHealthDisplayStrategy healthBar = GetComponent<AbstractEnemyHealthDisplayStrategy>();
        if (healthBar == null)
        {
            throw new System.Exception("There is no component that implements the AbstractEnemyHealthBar abstract class.");
        }

        Animator animator = GetComponent<Animator>();
        if (animator == null)
        {
            throw new System.Exception("There is no Animator component.");
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            throw new System.Exception("There is no SpriteRenderer component.");
        }

        enemyModelStrategy = Instantiate(pAbstractEnemyModelStrategy);
        enemyModelStrategy.Initialize(gameObject, healthBar, navMeshAgent, animator, spriteRenderer);
        hiddenAbstractEnemyModelStrategyChangeCheck = enemyModelStrategy;
    }

    /// <summary>
    /// Initializes the path finding model by instantiating a copy of its SO. Starts or stops the nav mesh agent accordingly
    /// </summary>
    private void InitializePathfindingModel(AbstractPathFindingStrategy pPathFindingStrategy)
    {
        if (pPathFindingStrategy == null)
        {
            if (navMeshAgent != null && navMeshAgent.isOnNavMesh)
                navMeshAgent.isStopped = true;
            pathFindingStrategy = null;
            hiddenAbstractPathFindingStrategyChangeCheck = null;
            return;
        }

        if (target == null)
        {
            throw new System.Exception("Target was not set.");
        }

        targetCircleCollider = target.GetComponent<CircleCollider2D>();
        if (targetCircleCollider == null)
        {
            throw new System.Exception("There is no CircleCollider2D component on target.");
        }

        if (navMeshAgent != null)
            navMeshAgent.isStopped = false;
        pathFindingStrategy = Instantiate(pPathFindingStrategy);
        pathFindingStrategy.Initialize(gameObject);
        hiddenAbstractPathFindingStrategyChangeCheck = pathFindingStrategy;
    }

    /// <summary>
    /// Calls the on fixed update function of the enemy model, calls move function
    /// </summary>
    private void FixedUpdate()
    {
        enemyModelStrategy.OnFixedUpdate();
        Move();
    }

    private void Update()
    {
        enemyModelStrategy.OnUpdate();
        CheckForScriptabeObjectChange();
        CheckDeath();
        CheckGoal();
    }

    /// <summary>
    /// Moves the path finding agent towards the goal
    /// </summary>
    private void Move()
    {
        if (target != null && pathFindingStrategy != null)
        {
            pathFindingStrategy.MoveTowardsTarget(target.transform.position, enemyModelStrategy.GetSpeed());
        }
    }

    /// <summary>
    /// Checks if SO is changed at runtime and initializes it again if so
    /// </summary>
    private void CheckForScriptabeObjectChange()
    {
        if (enemyModelStrategy != hiddenAbstractEnemyModelStrategyChangeCheck)
        {
            InitializeEnemyModel(enemyModelStrategy);
        }
        if (pathFindingStrategy != hiddenAbstractPathFindingStrategyChangeCheck)
        {
            InitializePathfindingModel(pathFindingStrategy);
        }
    }

    /// <summary>
    /// Checks wether health reaches 0, destroys the enemy and raises enemy dies event if so
    /// </summary>
    private void CheckDeath()
    {
        if (enemyModelStrategy.GetHealth() <= 0)
        {
            enemyDiesEvent.Raise(enemyModelStrategy.GetCarriedMoney(), this);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Checks wether the enemy reaches goal, destroys the enemy and raises enemy reaches goal event if so
    /// </summary>
    private void CheckGoal()
    {
        if (targetCircleCollider != null && capsuleCollider2D.IsTouching(targetCircleCollider))
        {
            enemyReachesGoalEvent.Raise(enemyModelStrategy.GetDamage(), this);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(enemyModelStrategy);
        Destroy(pathFindingStrategy);
    }
}
