using UnityEngine;

/// <summary>
/// Helper class for receiving on frame functions form weapon animations. On frame functions can only call functions of a class, the
/// animator is on. In this case, I need to call a function of a class on the parent object though. This class passes the information along
/// </summary>
public class WeaponAnimationReceiver : MonoBehaviour
{
    private AbstractAttackStrategy attackStrategy; //Attack strategy that contains the function which the on frame funcion of the animator needs to call

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Gets component
    /// </summary>
    private void Initialize()
    {
        attackStrategy = GetComponentInParent<TowerController>().GetAttackStrategy();
        if (attackStrategy == null)
        {
            throw new System.Exception("There is no TowerController component in parent.");
        }
    }

    /// <summary>
    /// Checks for switching of the attack strategy
    /// </summary>
    private void Update()
    {
        CheckForScriptabeObjectChange();
    }

    /// <summary>
    /// Always gets current attack strategy in case it is changed at runtime
    /// </summary>
    private void CheckForScriptabeObjectChange()
    {
        TowerController towerController = GetComponentInParent<TowerController>();
        if (towerController == null)
        {
            throw new System.Exception("There is no TowerController component in parent.");
        }

        attackStrategy = towerController.GetAttackStrategy();
    }

    /// <summary>
    /// Passes the attack on frame function
    /// </summary>
    private void Attack()
    {
        attackStrategy.Attack();
    }

    /// <summary>
    /// Passes the on first frame on frame function
    /// </summary>
    private void OnFirstFrame()
    {
        attackStrategy.OnFirstFrame();
    }

    private void OnLastFrame()
    {
        attackStrategy.OnLastFrame();
    }

    private void OnFifthFrame()
    {
        attackStrategy.OnFifthFrame();
    }
}
