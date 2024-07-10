using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Abstract class for enemy model strategy. Holds all base values of the enemy
/// If the enemy is hit by a projectile, either the do damage or do slow functions are called. Do damage reduces health and calls the
/// health displaying strategy to display the current health of the enemy. Do slow reduces the movement speed and sets the color of the 
/// sprite renderer to a slow color. Resets both after the slow duration is over.
/// This class also contains the instant kill enemies singleton and instantly destroys the game object if the debug option is toggled
/// </summary>
public abstract class AbstractEnemyModelStrategy : ScriptableObject
{
    [SerializeField]
    protected float speed; //Movement speed of enemy
    [SerializeField]
    protected int health; //Health of enemy
    [SerializeField]
    protected int carriedMoney; //Money earned, if enemy is killed
    [SerializeField]
    protected int damage; //Damage that enemy does, if it reaches goal
    [SerializeField]
    [ColorUsage(true, true)]
    protected Color slowColorMaterial; //Color of enemy while slowed
    [SerializeField]
    protected Color slowColorLight; //Color of enemy while slowed
    [SerializeField]
    protected float slowLightIntensityIncrease;
    [SerializeField]
    protected ToggleInstantKillEnemiesSingleton toggleInstantKillEnemiesSingleton; //Singleton scriptable object for debug option of toggling to instant kill enemy

    protected GameObject gameObject;
    protected AbstractEnemyHealthDisplayStrategy healthDisplayingStrategy; //Heal displaying strategy
    protected NavMeshAgent navMeshAgent; //Navmesh agent component
    protected Animator animator; //Animator component
    protected SpriteRenderer spriteRenderer; //Sprite renderer component
    protected Light2D light;
    protected Material material;
    protected Color initialColor; //Initial color of enemy
    protected Color initialLightColor;
    [ColorUsage(true, true)] protected Color initialMaterialColor;
    protected float initialSpeed; //Initial speed of enemy
    protected int maxHealth; //Maximum health of enemy
    protected bool isSlowed; //Bool of slow status of enemy
    protected float totalSlowDuration; //Total duration of current slow
    protected float currSlowDuration; //Current duration of current slow

    /// <summary>
    /// Initializes values
    /// </summary>
    public void Initialize(GameObject pGameObject, AbstractEnemyHealthDisplayStrategy pHealthBar, NavMeshAgent pNavMeshAgent,
        Animator pAnimator, SpriteRenderer pSpriteRenderer)
    {
        gameObject = pGameObject;
        healthDisplayingStrategy = pHealthBar;
        navMeshAgent = pNavMeshAgent;
        animator = pAnimator;
        spriteRenderer = pSpriteRenderer;
        initialSpeed = speed;
        initialColor = spriteRenderer.color;
        isSlowed = false;
        maxHealth = health;
        if (healthDisplayingStrategy != null)
            healthDisplayingStrategy.SetHealth(health, maxHealth);

        light = gameObject.GetComponentInChildren<Light2D>();
        if (light == null)
        {
            throw new System.Exception("There is no Light2D component.");
        }
        initialLightColor = light.color;

        material = pSpriteRenderer.material;
        initialMaterialColor = material.GetColor("_Color");
    }

    /// <summary>
    /// Does damage to the enemy, updates enemy health displaying strategy
    /// </summary>
    public virtual void DoDamage(int pDamage)
    {
        health -= pDamage;
        if (toggleInstantKillEnemiesSingleton != null && toggleInstantKillEnemiesSingleton.GetInstantKillEnemies())
            health = 0;
        if(healthDisplayingStrategy != null)
            healthDisplayingStrategy.SetHealth(health, maxHealth);
    }

    /// <summary>
    /// Slows the enemy, sets slow color, starts counter
    /// </summary>
    public virtual void DoSlow(float pSlowPercentage, float pSlowDuration)
    {
        material.SetInt("_IsSlowed", 1);
        light.color = slowColorLight;
        light.intensity *= slowLightIntensityIncrease;
        material.SetColor("_Color", slowColorMaterial);
        isSlowed = true;
        currSlowDuration = 0;
        totalSlowDuration = pSlowDuration;
        speed = initialSpeed * ((100 - pSlowPercentage) / 100);
        navMeshAgent.speed = speed;
        animator.speed = speed;
    }

    /// <summary>
    /// Returns velocity of enemies nav mesh agent component
    /// </summary>
    public Vector3 GetVelocity()
    {
        if (navMeshAgent == null)
            return new(0,0,0);
        return navMeshAgent.velocity;
    }

    /// <summary>
    /// Returns health field
    /// </summary>
    public int GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Returns carried money field
    /// </summary>
    public int GetCarriedMoney()
    {
        return carriedMoney;
    }

    /// <summary>
    /// Returns damage field
    /// </summary>
    public int GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// Returns speed field
    /// </summary>
    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// Called on every fixed update
    /// </summary>
    public virtual void OnFixedUpdate()
    {
        if (isSlowed)
            CountDownSlow();
    }

    /// <summary>
    /// Called on every update
    /// </summary>
    public virtual void OnUpdate()
    {
    }

    /// <summary>
    /// Counts ddown slow, undoes slow effect if slow is over
    /// </summary>
    protected virtual void CountDownSlow()
    {
        currSlowDuration += Time.fixedDeltaTime;
        if (currSlowDuration > totalSlowDuration)
        {
            isSlowed = false;
            speed = initialSpeed;
            navMeshAgent.speed = speed;
            animator.speed = speed;
            totalSlowDuration = 0;
            currSlowDuration = 0;
            light.color = initialLightColor;
            light.intensity /= slowLightIntensityIncrease;
            material.SetColor("_Color", initialMaterialColor);
            material.SetInt("_IsSlowed", 0);
        }
    }
}
