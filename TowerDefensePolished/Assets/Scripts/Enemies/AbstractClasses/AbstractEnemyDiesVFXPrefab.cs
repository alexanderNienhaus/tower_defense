using UnityEngine;

/// <summary>
/// Abstract class for displaying the enemy dies vfx in a prefab
/// </summary>
public abstract class AbstractEnemyDiesVFXPrefab : MonoBehaviour
{
    protected float currentTime; //Time counter

    /// <summary>
    /// Abstract method DisplayText, will be different for each concrete enemy dies vfy displaying strategy prefab
    /// </summary>
    public abstract void DisplayText(string pText, Vector3 pPosition, float pTimeToLive);
}
