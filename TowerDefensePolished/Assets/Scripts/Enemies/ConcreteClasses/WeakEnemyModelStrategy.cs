using UnityEngine;

[CreateAssetMenu(fileName = "WeakEnemyModelStrategy",
    menuName = "Scriptable Objects/Enemies/WeakEnemyModelStrategy")]
/// <summary>
/// Concrete Scriptable object for the weak enemy model. Does not differ from abstract class at the moment
/// </summary>
public class WeakEnemyModelStrategy : AbstractEnemyModelStrategy
{
    [SerializeField]
    private ParticleSystem particleSystemPrefab;

    public override void OnUpdate()
    {
        if (health <= 0)
        {
            Instantiate(particleSystemPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
}
