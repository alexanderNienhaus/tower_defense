using UnityEngine;

[CreateAssetMenu(fileName = "StrongEnemyModelStrategy",
    menuName = "Scriptable Objects/Enemies/StrongEnemyModelStrategy")]
/// <summary>
/// Concrete Scriptable object for the strong enemy model
/// </summary>
public class StrongEnemyModelStrategy : AbstractEnemyModelStrategy
{
    [SerializeField]
    private ParticleSystem deathSplatterBigParticleSystemPrefab;

    [SerializeField]
    private ParticleSystem deathSplatterExtraParticleSystemPrefab;

    public override void OnUpdate()
    {
        if (health <= 0)
        {
            Instantiate(deathSplatterBigParticleSystemPrefab, gameObject.transform.position, Quaternion.identity);
            Instantiate(deathSplatterExtraParticleSystemPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
}
