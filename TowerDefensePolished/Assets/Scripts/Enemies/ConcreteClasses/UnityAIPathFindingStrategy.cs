using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "UnityAIPathFindingStrategy",
    menuName = "Scriptable Objects/Path Finding Strategies/UnityAIPathFindingStrategy")]
/// <summary>
/// Concrete scriptable object for the path finding stratefy, uses Unity AI
/// </summary>
public class UnityAIPathFindingStrategy : AbstractPathFindingStrategy
{
    private NavMeshAgent agent; //Navmesh agent component

    /// <summary>
    /// Concrete implementation of the Initialize method, sets the navmesh agent component
    /// </summary>
    public override void Initialize(GameObject pGameObject)
    {
        agent = pGameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            throw new System.Exception("There is no NavMeshAgent component.");
        }
    }

    /// <summary>
    /// Concrete implementation of the MoveTowardsTarget method, moves the object towards the destination, using Unity AI
    /// </summary>
    public override void MoveTowardsTarget(Vector3 pTargetPosition, float pSpeed)
    {
        if (agent == null)
            return;

        agent.speed = pSpeed;
        agent.SetDestination(pTargetPosition);
    }
}
