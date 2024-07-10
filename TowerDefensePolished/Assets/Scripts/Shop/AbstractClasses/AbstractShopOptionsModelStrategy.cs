using UnityEngine;

/// <summary>
/// Abstract class for creating shop options, see basic shop options model strategy class for concrete implementation
/// </summary>
public abstract class AbstractShopOptionsModelStrategy : MonoBehaviour
{
    protected AbstractShopOptionsDisplayStrategy shopOptionsDisplayStrategy; //Strategy for displaying shop options
    protected TowerController tower; //Tower of selected positions (is null on an empty position)
    protected Vector3 position; //Selected positions

    /// <summary>
    /// Initialize strategy
    /// </summary>
    public virtual void Initialize(AbstractShopOptionsDisplayStrategy pAbstractShopOptionDisplayer)
    {
        shopOptionsDisplayStrategy = pAbstractShopOptionDisplayer;
    }

    /// <summary>
    /// Shows the specific shop option
    /// </summary>
    public virtual void ShowShopOptions(Vector3 pPosition, TowerController pTower)
    {
        tower = pTower;
        position = pPosition;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pPosition);
        
        if(tower != null)
        {
            AbstractTowerModelStrategy towerModelStrategy = pTower.GetTowerModelStrategy();
            if (towerModelStrategy.GetTowerLevel() == TowerLevel.Standard)
            {
                GetStandardTowerOptions(tower, screenPos);
            }
            else if (towerModelStrategy.GetTowerLevel() == TowerLevel.Upgraded)
            {
                GetUpgradedTowerOptions(tower, screenPos);
            }
        }
        else
        {
            GetEmptyTowerOptions(screenPos);
        }
    }

    /// <summary>
    /// Abstract function of GetCorrectTowerPrefab. Will be different for each concrete shop options model strategy
    /// </summary>
    public abstract TowerController GetCorrectTowerPrefab(ShopAction pShopAction, TowerType pTowerType = TowerType.None);

    /// <summary>
    /// Abstract function of GetTowerPosition. Will be different for each concrete shop options model strategy
    /// </summary>
    public abstract Vector3 GetTowerPosition();

    /// <summary>
    /// Abstract function of GetEmptyTowerOptions. Will be different for each concrete shop options model strategy
    /// </summary>
    protected abstract void GetEmptyTowerOptions(Vector3 pScreenPos);

    /// <summary>
    /// Abstract function of GetStandardTowerOptions. Will be different for each concrete shop options model strategy
    /// </summary>
    protected abstract void GetStandardTowerOptions(TowerController pTowerController, Vector3 pScreenPos);

    /// <summary>
    /// Abstract function of GetUpgradedTowerOptions. Will be different for each concrete shop options model strategy
    /// </summary>
    protected abstract void GetUpgradedTowerOptions(TowerController pTowerController, Vector3 pScreenPos);
}
