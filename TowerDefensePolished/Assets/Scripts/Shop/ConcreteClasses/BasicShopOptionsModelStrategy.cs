using UnityEngine;

/// <summary>
/// Concrete class for creation of shop option elements. Takes values of towers and formats them to the correct shop options.
/// This class is called by the shop options controller and creates the shop option elements. It formats the GUI strings correctly, based
/// on what tower stats need to be dispalyed and calls the correct dispalying methods of the shop options display strategy
/// </summary>
public class BasicShopOptionsModelStrategy : AbstractShopOptionsModelStrategy
{
    [SerializeField]
    protected TowerController attackTowerStandardPrefab; //Prefab for standard single attack tower
    [SerializeField]
    protected TowerController attackTowerUpgradedPrefab; //Prefab for upgraded single attack tower
    [SerializeField]
    protected TowerController aoeTowerStandardPrefab; //Prefab for standard aoe tower
    [SerializeField]
    protected TowerController aoeTowerUpgradedPrefab; //Prefab for upgraded aoe tower
    [SerializeField]
    protected TowerController debuffTowerStandardPrefab; //Prefab for standard debuff tower
    [SerializeField]
    protected TowerController debuffTowerUpgradedPrefab; //Prefab for upgraded debuff tower

    [SerializeField]
    protected string attackTowerStatsString; //String for displaying the stats of the single attack tower
    [SerializeField]
    protected string aoeTowerStatsString; //String for displaying the stats of the aoe tower
    [SerializeField]
    protected string debuffTowerStatsString; //String for displaying the stats of the debuff tower

    /// <summary>
    /// Formats the GUI string for the single attack tower
    /// </summary>
    public string FormatStandardSTTowerStats(STTowerModelStrategy pSTModelStrategy, STAttackStrategyStandard pSTAttackStrategy)
    {
        return string.Format(attackTowerStatsString, GetTowerTypeString(pSTModelStrategy.GetTowerType()), GetTowerLevelString(pSTModelStrategy.GetTowerLevel()),
            pSTModelStrategy.GetBuyCost(), pSTModelStrategy.GetSellValue(), pSTAttackStrategy.GetDamage(), pSTAttackStrategy.GetAttackSpeed(), pSTAttackStrategy.GetRange());
    }

    /// <summary>
    /// Formats the GUI string for the single attack tower
    /// </summary>
    public string FormatUpgradedSTTowerStats(STTowerModelStrategy pSTModelStrategy, STAttackStrategyUpgraded pSTAttackStrategy)
    {
        return string.Format(attackTowerStatsString, GetTowerTypeString(pSTModelStrategy.GetTowerType()), GetTowerLevelString(pSTModelStrategy.GetTowerLevel()),
            pSTModelStrategy.GetBuyCost(), pSTModelStrategy.GetSellValue(), pSTAttackStrategy.GetDamage(), pSTAttackStrategy.GetAttackSpeed(), pSTAttackStrategy.GetRange());
    }

    /// <summary>
    /// Formats the GUI string for the aoe tower
    /// </summary>
    public string FormatStandardAoeTowerStats(AoeTowerModelStrategy pAoeTowerModelStrategy, AoeAttackStrategyStandard pAoeTowerAttackStrategy)
    {
        return string.Format(aoeTowerStatsString, GetTowerTypeString(pAoeTowerModelStrategy.GetTowerType()), GetTowerLevelString(pAoeTowerModelStrategy.GetTowerLevel()),
            pAoeTowerModelStrategy.GetBuyCost(), pAoeTowerModelStrategy.GetSellValue(), pAoeTowerAttackStrategy.GetDamage(), pAoeTowerAttackStrategy.GetAttackSpeed(), pAoeTowerAttackStrategy.GetRange());
    }

    /// <summary>
    /// Formats the GUI string for the aoe tower
    /// </summary>
    public string FormatUpgradedAoeTowerStats(AoeTowerModelStrategy pAoeTowerModelStrategy, AoeAttackStrategyUpgraded pAoeTowerAttackStrategy)
    {
        return string.Format(aoeTowerStatsString, GetTowerTypeString(pAoeTowerModelStrategy.GetTowerType()), GetTowerLevelString(pAoeTowerModelStrategy.GetTowerLevel()),
            pAoeTowerModelStrategy.GetBuyCost(), pAoeTowerModelStrategy.GetSellValue(), pAoeTowerAttackStrategy.GetDamage(), pAoeTowerAttackStrategy.GetAttackSpeed(), pAoeTowerAttackStrategy.GetRange());
    }

    /// <summary>
    /// Formats the GUI string for the debuff tower
    /// </summary>
    public string FormatDebuffTowerStats(DebuffTowerModelStrategy pDebuffTowerModelStrategy, DebuffAttackStrategy pDebuffTowerAttackStrategy)
    {
        return string.Format(debuffTowerStatsString, GetTowerTypeString(pDebuffTowerModelStrategy.GetTowerType()), GetTowerLevelString(pDebuffTowerModelStrategy.GetTowerLevel()),
            pDebuffTowerModelStrategy.GetBuyCost(), pDebuffTowerModelStrategy.GetSellValue(), pDebuffTowerAttackStrategy.GetSlowStrength(), pDebuffTowerAttackStrategy.GetSlowDuration(),
            pDebuffTowerAttackStrategy.GetAttackSpeed(), pDebuffTowerAttackStrategy.GetRange());
    }

    /// <summary>
    /// Returns the correct tower prefab, based on the shop action
    /// </summary>
    public override TowerController GetCorrectTowerPrefab(ShopAction pShopAction, TowerType pTowerType = TowerType.None)
    {
        TowerController towerPrefab = null;
        switch (pShopAction)
        {
            case ShopAction.Buy:
                switch (pTowerType)
                {
                    case TowerType.Attack:
                        towerPrefab = attackTowerStandardPrefab;
                        break;
                    case TowerType.Aoe:
                        towerPrefab = aoeTowerStandardPrefab;
                        break;
                    case TowerType.Debuff:
                        towerPrefab = debuffTowerStandardPrefab;
                        break;
                }
                break;
            case ShopAction.Upgrade:
                switch (tower.GetTowerModelStrategy().GetTowerType())
                {
                    case TowerType.Attack:
                        towerPrefab = attackTowerUpgradedPrefab;
                        break;
                    case TowerType.Aoe:
                        towerPrefab = aoeTowerUpgradedPrefab;
                        break;
                    case TowerType.Debuff:
                        towerPrefab = debuffTowerUpgradedPrefab;
                        break;
                }
                break;
            case ShopAction.Sell:
                towerPrefab = tower;
                break;
        }

        return towerPrefab;
    }

    /// <summary>
    /// Returns the position of the tower building spot that was clicked
    /// </summary>
    public override Vector3 GetTowerPosition()
    {
        return position;
    }

    /// <summary>
    /// Creates the shop element for empty tower options
    /// </summary>
    protected override void GetEmptyTowerOptions(Vector3 pScreenPos)
    {
        shopOptionsDisplayStrategy.DisplayEmptyShopOption(pScreenPos);

        shopOptionsDisplayStrategy.DisplayAttackTowerText(FormatStandardSTTowerStats(
            (STTowerModelStrategy)attackTowerStandardPrefab.GetTowerModelStrategy(), (STAttackStrategyStandard)attackTowerStandardPrefab.GetAttackStrategy()));
        shopOptionsDisplayStrategy.DisplayAoeTowerText(FormatStandardAoeTowerStats(
            (AoeTowerModelStrategy)aoeTowerStandardPrefab.GetTowerModelStrategy(), (AoeAttackStrategyStandard)aoeTowerStandardPrefab.GetAttackStrategy()));        
        shopOptionsDisplayStrategy.DisplayDebuffTowerText(FormatDebuffTowerStats(
            (DebuffTowerModelStrategy)debuffTowerStandardPrefab.GetTowerModelStrategy(), (DebuffAttackStrategy)debuffTowerStandardPrefab.GetAttackStrategy()));
    }

    /// <summary>
    /// Creates the shop element for standard tower options
    /// </summary>
    protected override void GetStandardTowerOptions(TowerController pTowerController, Vector3 pScreenPos)
    {
        shopOptionsDisplayStrategy.DisplayStandardShopOption(pScreenPos);

        switch (pTowerController.GetTowerModelStrategy().GetTowerType())
        {
            case TowerType.Attack:
                shopOptionsDisplayStrategy.DisplaySellTowerText(FormatStandardSTTowerStats(
                    (STTowerModelStrategy)attackTowerStandardPrefab.GetTowerModelStrategy(), (STAttackStrategyStandard)attackTowerStandardPrefab.GetAttackStrategy()));
                shopOptionsDisplayStrategy.DisplayUpgradeTowerText(FormatUpgradedSTTowerStats(
                    (STTowerModelStrategy)attackTowerUpgradedPrefab.GetTowerModelStrategy(), (STAttackStrategyUpgraded)attackTowerUpgradedPrefab.GetAttackStrategy()));
                break;
            case TowerType.Aoe:
                shopOptionsDisplayStrategy.DisplaySellTowerText(FormatStandardAoeTowerStats(
                    (AoeTowerModelStrategy)aoeTowerStandardPrefab.GetTowerModelStrategy(), (AoeAttackStrategyStandard)aoeTowerStandardPrefab.GetAttackStrategy()));
                shopOptionsDisplayStrategy.DisplayUpgradeTowerText(FormatUpgradedAoeTowerStats(
                    (AoeTowerModelStrategy)aoeTowerUpgradedPrefab.GetTowerModelStrategy(), (AoeAttackStrategyUpgraded)aoeTowerUpgradedPrefab.GetAttackStrategy()));
                break;
            case TowerType.Debuff:
                shopOptionsDisplayStrategy.DisplaySellTowerText(FormatDebuffTowerStats(
                    (DebuffTowerModelStrategy)debuffTowerStandardPrefab.GetTowerModelStrategy(), (DebuffAttackStrategy)debuffTowerStandardPrefab.GetAttackStrategy()));
                shopOptionsDisplayStrategy.DisplayUpgradeTowerText(FormatDebuffTowerStats(
                    (DebuffTowerModelStrategy)debuffTowerUpgradedPrefab.GetTowerModelStrategy(), (DebuffAttackStrategy)debuffTowerUpgradedPrefab.GetAttackStrategy()));
                break;
        }
    }

    /// <summary>
    /// Creates the shop element for upgraded tower options
    /// </summary>
    protected override void GetUpgradedTowerOptions(TowerController pTowerController, Vector3 pScreenPos)
    {
        shopOptionsDisplayStrategy.DisplayUpgradedShopOption(pScreenPos);

        switch (pTowerController.GetTowerModelStrategy().GetTowerType())    
        {
            case TowerType.Attack:
                shopOptionsDisplayStrategy.DisplaySellUpgradedTowerText(FormatUpgradedSTTowerStats(
                    (STTowerModelStrategy)attackTowerUpgradedPrefab.GetTowerModelStrategy(), (STAttackStrategyUpgraded)attackTowerUpgradedPrefab.GetAttackStrategy()));
                break;
            case TowerType.Aoe:
                shopOptionsDisplayStrategy.DisplaySellUpgradedTowerText(FormatUpgradedAoeTowerStats(
                    (AoeTowerModelStrategy)aoeTowerUpgradedPrefab.GetTowerModelStrategy(), (AoeAttackStrategyUpgraded)aoeTowerUpgradedPrefab.GetAttackStrategy()));
                break;
            case TowerType.Debuff:
                shopOptionsDisplayStrategy.DisplaySellUpgradedTowerText(FormatDebuffTowerStats(
                    (DebuffTowerModelStrategy)debuffTowerUpgradedPrefab.GetTowerModelStrategy(), (DebuffAttackStrategy)debuffTowerUpgradedPrefab.GetAttackStrategy()));
                break;
        }
    }

    /// <summary>
    /// Returns the tower type as a string
    /// </summary>
    private string GetTowerTypeString(TowerType pTowerType)
    {
        string str = "";
        switch (pTowerType)
        {
            case TowerType.Attack:
                str = "Attack";
                break;
            case TowerType.Aoe:
                str = "Aoe";
                break;
            case TowerType.Debuff:
                str = "Debuff";
                break;
        }
        return str;
    }

    /// <summary>
    /// Returns the tower level as a string
    /// </summary>
    private string GetTowerLevelString(TowerLevel pTowerLevel)
    {
        string str = "";
        switch (pTowerLevel)
        {
            case TowerLevel.Standard:
                str = "Standard";
                break;
            case TowerLevel.Upgraded:
                str = "Upgraded";
                break;
        }
        return str;
    }
}
