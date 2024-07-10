using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for attackers. Scans for targets, turns the tower accordingly and spawns projectile in interval.
/// Uses the alive enemies singleton to keep track of alive enemies. Searches one which is furtest ahead (highest x pos) and in range
/// and sets it as targets. Targets are attacked as long as they are in range, if they are out of range or die, a new target will be searched.
/// The weapon of the tower will be slerped to an angle where a shoot will hit. If the tower does not have a target, the weapon will slerp
/// to its original position after a while. Most methods can be overwritten by the concrete attackers since this is an abstract class, but
/// most of the functionality will be the same for all. The create projectile method must be overwritten though because the projectile
/// is guaranteed to differ between attackers.
/// </summary>
public abstract class AbstractAttackStrategy : ScriptableObject
{
    [SerializeField]
    protected AbstractProjectile projectilePrefab; //Prefab of projectile to be spawned
    [SerializeField]
    protected float attackRange; //Detection range of enemies
    [SerializeField]
    protected float attackSpeed; //Attack interval in which projectiles are spawned
    [SerializeField]
    protected float projectileSpeed; //Move speed of projectile
    [SerializeField]
    protected float rotationSpeed; //Rotation speed of tower weapon
    [SerializeField]
    protected float timeTillResetToStandardPosition; //Time without target after that the weapon will return to standard rotation
    [SerializeField]
    protected float requiredAttackAngle; //Minimum required angle between weapon and target before attack animation can start
    [SerializeField]
    protected float middleOfScreenHeight; //Middle of the screen, used set the weapons initial rotation towards middle of screen
    [SerializeField]
    protected AliveEnemiesSingleton aliveEnemiesSingleton; //Singleton that contains a list of all alive enemies

    protected GameObject gameObject;
    protected Transform weapon; //Weapon of the tower. Contains the animator
    protected Animator animator; //Animator of weapon
    protected EnemyController currentTarget; //Current target the tower is aiming and shooting at
    protected Quaternion originalWeaponRotation; //Original rotation of weapon
    protected float timeCount; //Time counter, used for slerp
    protected float timeWithoutTarget; //Time span during which the tower had no target
    protected float slowRotationSpeed;

    /// <summary>
    /// Gets components and sets initial values. Almost all functions may be overridden by concrete attacker
    /// </summary>
    public virtual void Initialize(GameObject pGameObject, Transform pTransform)
    {
        gameObject = pGameObject;
        weapon = pTransform.GetChild(0);
        if (weapon == null)
        {
            throw new System.Exception("There is no weapon child.");
        }

        animator = weapon.GetComponent<Animator>();
        if (animator == null)
        {
            throw new System.Exception("There is no Animator component in weapon.");
        }
        animator.speed = 0;
        slowRotationSpeed = 1;
    }

    /// <summary>
    /// Called on start by tower controler
    /// </summary>
    public virtual void OnStart()
    {
        if (weapon.position.y > middleOfScreenHeight)
        {
            weapon.Rotate(new(0, 0, 180));
        }
        originalWeaponRotation = weapon.rotation;
    }

    /// <summary>
    /// Creates a projectile. Called on animation frame at which the projectile needs to be created
    /// </summary>
    public virtual void Attack()
    {
        CreateProjectile(aliveEnemiesSingleton.GetAliveEnemies(), weapon.rotation, projectileSpeed);
    }

    /// <summary>
    /// Stops the animtion on first frame
    /// </summary>
    public virtual void OnFirstFrame()
    {
        animator.speed = 0;
    }

    public virtual void OnLastFrame()
    {
    }

    public virtual void OnFifthFrame()
    {
    }

    /// <summary>
    /// Returns attack speed field
    /// </summary>
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    /// <summary>
    /// Returns attack range field
    /// </summary>
    public float GetRange()
    {
        return attackRange;
    }

    /// <summary>
    /// Checks if current target is still in range, searches new one if not. Slerps either to target or original position
    /// </summary>
    public virtual void OnFixedUpdate()
    {
        if (currentTarget != null)
        {
            SlerpToTarget(rotationSpeed);
        }
        else
        {
            SlerpToOriginalRotation(slowRotationSpeed);
        }
    }

    /// <summary>
    /// May be overriden by concrete attacker
    /// </summary>
    public virtual void OnUpdate()
    {
        if (weapon == null || animator == null)
            return;

        if (currentTarget == null || !CheckInRange(currentTarget.transform.position))
        {
            if (!SearchNewTarget())
            {
                currentTarget = null;
            }
        }
    }

    /// <summary>
    /// Slerp the weapon towards the target, activate the animator if the required angle is reached
    /// </summary>
    protected virtual void SlerpToTarget(float pRotationSpeed)
    {
        Quaternion rotationToTargetsFuturePosition = GetRotationToTargetsFuturePostion(weapon, currentTarget);
        if (Quaternion.Angle(rotationToTargetsFuturePosition, weapon.transform.rotation) <= requiredAttackAngle)
        {
            animator.speed = attackSpeed;
        }
        weapon.transform.rotation = Quaternion.Slerp(weapon.rotation, rotationToTargetsFuturePosition, pRotationSpeed * Time.deltaTime);
        timeCount +=  Time.fixedDeltaTime;
        timeWithoutTarget = 0;
    }

    /// <summary>
    /// Slerp to the original weapon rotation, count the time without target
    /// </summary>
    protected virtual void SlerpToOriginalRotation(float pRotationSpeed)
    {
        timeWithoutTarget += Time.deltaTime;
        if (timeWithoutTarget > timeTillResetToStandardPosition)
        {
            weapon.transform.rotation = Quaternion.Slerp(weapon.rotation, originalWeaponRotation, pRotationSpeed * Time.deltaTime);
            timeCount += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Return if a position is within attack range
    /// </summary>
    protected virtual bool CheckInRange(Vector3 pPosition)
    {
        return attackRange >= (pPosition - weapon.position).magnitude;
    }

    /// <summary>
    /// Check for all alive enemies if they are in range, set new target if an enemy in range is found
    /// Prefers enemies that are further ahead
    /// </summary>
    protected virtual bool SearchNewTarget()
    {
        bool targetFound = false;
        float xOfTarget = Camera.main.ScreenToWorldPoint(new(Screen.safeArea.xMin, 0, 0)).x;
        foreach (EnemyController target in aliveEnemiesSingleton.GetAliveEnemies())
        {
            if (target == null)
                continue;

            Vector3 targetPosition = target.transform.position;
            if (CheckInRange(targetPosition) && xOfTarget <= targetPosition.x)
            {

                xOfTarget = targetPosition.x;
                currentTarget = target;
                targetFound = true;
            }
        }
        return targetFound;
    }

    /// <summary>
    /// Get the required rotation that the weapon needs to have, so that a projectile will hit an enemy if both travel at constant velocity
    /// </summary>
    protected virtual Quaternion GetRotationToTargetsFuturePostion(Transform pFrom, EnemyController pTo)
    {
        Vector2 fromPos = pFrom.position;
        Vector2 toPos = pTo.transform.position;

        if (projectileSpeed != 0)
        {
            float projectileFlightTime = (toPos - fromPos).magnitude / projectileSpeed;
            Vector3 targetVelocity = pTo.GetVelocity();
            Vector2 targetvelocity = new(targetVelocity.x, targetVelocity.y);
            toPos += targetvelocity * projectileFlightTime;
        }
        float angle = Mathf.Atan2(toPos.y - fromPos.y, toPos.x - fromPos.x) * Mathf.Rad2Deg - 90.0f;
        return Quaternion.Euler(new(0, 0, angle));
    }

    /// <summary>
    /// Abstract function of CreateProjectile. Will be different for each concrete attack strategy
    /// </summary>
    protected abstract void CreateProjectile(List<EnemyController> pTargets, Quaternion pRotation, float pSpeed);
}
