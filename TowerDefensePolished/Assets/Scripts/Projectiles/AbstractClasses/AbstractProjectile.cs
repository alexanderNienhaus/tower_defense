using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for projecties. Moves the projectile and checks for hits with enemies. Most methods can be overriden by the concrete
/// implementations of this class, but most functionality will be the same.
/// </summary>
public abstract class AbstractProjectile : MonoBehaviour
{
    protected Rigidbody2D rigidBody2D; //Rigidbody component of projectile
    protected CapsuleCollider2D capsuleCollider2D; //Capsule collider 2D component of projectile
    protected Animator animator; //Animator component of projectile
    protected List<EnemyController> targets; //List of targets
    protected float speed; //Speed of projectile

    protected virtual void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize values
    /// </summary>
    protected void Initialize()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        if (rigidBody2D == null)
        {
            throw new System.Exception("There is no RigidBody2D component.");
        }

        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if (capsuleCollider2D == null)
        {
            throw new System.Exception("There is no CapsuleCollider2D component.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            throw new System.Exception("There is no Animator component.");
        }
    }

    /// <summary>
    /// May be overriden by conrete class
    /// </summary>
    protected virtual void FixedUpdate()
    {
        Move();
        CheckForHit();
    }

    /// <summary>
    /// Moves the projectiler
    /// </summary>
    protected virtual void Move()
    {
        rigidBody2D.MovePosition(rigidBody2D.position + new Vector2(transform.up.x, transform.up.y) * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Checks for a collision with all alive enemies
    /// </summary>
    protected virtual void CheckForHit()
    {
        foreach (EnemyController target in targets)
        {
            if (target == null)
                continue;

            CapsuleCollider2D targetCapsuleCollider2D = target.GetComponent<CapsuleCollider2D>();
            if (targetCapsuleCollider2D != null)
            {
                if (capsuleCollider2D.IsTouching(targetCapsuleCollider2D))
                {
                    EnemyHit(target);
                }
            }
            else
            {
                throw new System.Exception("There is no CapsuleCollider2D component on target.");
            }
        }
    }

    /// <summary>
    /// Abstract function EnemyHit. Will be different for each concrete projectile
    /// </summary>
    protected abstract void EnemyHit(EnemyController pEnemyController);
}
