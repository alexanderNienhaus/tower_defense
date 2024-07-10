using UnityEngine;

/// <summary>
/// Enum for different types of towers
/// </summary>
public enum TowerType
{
    Attack,
    Aoe,
    Debuff,
    None
}

/// <summary>
/// Enum for different levels of towers
/// </summary>
public enum TowerLevel
{
    Standard,
    Upgraded
}

/// <summary>
/// Abstract class for the tower model. Represents all tower fields that have nothing to do with attacking, those can be found
/// in the abstract attack strategy.
/// </summary>
public abstract class AbstractTowerModelStrategy : ScriptableObject
{
    [SerializeField]
    protected TowerType towerType; //Type of tower
    [SerializeField]
    protected TowerLevel towerLevel; //Level of tower
    [SerializeField]
    protected int buyCost; //Cost of tower (for upgraded towers this is basically the upgrade cost)
    [SerializeField]
    protected int sellValue; //Sell value of tower

    /// <summary>
    /// Returns tower type field
    /// </summary>
    public TowerType GetTowerType()
    {
        return towerType;
    }

    /// <summary>
    /// Returns tower level field
    /// </summary>
    public TowerLevel GetTowerLevel()
    {
        return towerLevel;
    }

    /// <summary>
    /// Returns buy cost field
    /// </summary>
    public int GetBuyCost()
    {
        return buyCost;
    }

    /// <summary>
    /// Returns sell value field
    /// </summary>
    public int GetSellValue()
    {
        return sellValue;
    }
}
