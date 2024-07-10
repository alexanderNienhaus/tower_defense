using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for aoe impacts. Checks for hits with enemies. Every enemy can only be hit once
/// See basic aoe impact class for concrete implementation
/// </summary>
public abstract class AbstractImpact : MonoBehaviour
{
    protected CircleCollider2D circleCollider2D; //Circle collider component
    protected List<EnemyController> targets = new List<EnemyController>(); //List of targets
    protected List<EnemyController> enemiesHit = new List<EnemyController>(); //List of enemies that have already been hit

    protected virtual void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Get component
    /// </summary>
    protected virtual void Initialize()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        if (circleCollider2D == null)
        {
            throw new System.Exception("There is no CircleCollider2D component.");
        }
    }

    protected virtual void Update()
    {
        CheckForHit();
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
                if (circleCollider2D.IsTouching(targetCapsuleCollider2D))
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
    /// Destroy impact when animation is over
    /// </summary>
    protected virtual void ImpactAnimationOver()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Abstract function of EnemyHit. Will be different for each concrete aoe impact
    /// </summary>
    protected abstract void EnemyHit(EnemyController pEnemyController);
}
