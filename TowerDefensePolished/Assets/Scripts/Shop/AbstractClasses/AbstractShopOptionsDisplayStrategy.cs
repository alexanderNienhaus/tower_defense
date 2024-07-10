using UnityEngine;

/// <summary>
/// Abstract class for displaying shop info elements, see tmpro shop info displaying strategy class for concrete implementation
/// </summary>
public abstract class AbstractShopOptionsDisplayStrategy : MonoBehaviour
{
    /// <summary>
    /// Abstract function of DisplayAttackTowerText. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplayAttackTowerText(string pText);

    /// <summary>
    /// Abstract function of DisplayAoeTowerText. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplayAoeTowerText(string pText);

    /// <summary>
    /// Abstract function of DisplayDebuffTowerText. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplayDebuffTowerText(string pText);

    /// <summary>
    /// Abstract function of DisplayUpgradeTowerText. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplayUpgradeTowerText(string pText);

    /// <summary>
    /// Abstract function of DisplaySellTowerText. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplaySellTowerText(string pText);

    /// <summary>
    /// Abstract function of DisplaySellUpgradedTowerText. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplaySellUpgradedTowerText(string pText);

    /// <summary>
    /// Abstract function of DisplayEmptyShopOption. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplayEmptyShopOption(Vector3 pScreenPos);

    /// <summary>
    /// Abstract function of DisplayStandardShopOption. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplayStandardShopOption(Vector3 pScreenPos);

    /// <summary>
    /// Abstract function of DisplayUpgradedShopOption. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void DisplayUpgradedShopOption(Vector3 pScreenPos);

    /// <summary>
    /// Abstract function of HideAllShopOptions. Will be different for each concrete shop options display strategy
    /// </summary>
    public abstract void HideAllShopOptions();
}
