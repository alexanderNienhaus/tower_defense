using UnityEngine;
using TMPro;

/// <summary>
/// Concrete class for shop option displaying, uses TMPro. This class is called by the shop options model strategy to dispaly tower stats
/// in the GUI and activate the correct GUI elements. If the player clicks away from the shop elements they will be deactivated.
/// Also, if a GUI element is created partially out of screen, this class moves it back into screen
/// </summary>
public class TMProShopOptionsDisplayStrategy : AbstractShopOptionsDisplayStrategy
{
    [SerializeField]
    private int distanceToHideElement; //If the mouse is clicked this distance away from an element, it is hidden

    [SerializeField]
    private RectTransform canvasRectTransform; //Recttransform of canvas
    [SerializeField]
    private RectTransform emptyTowerOptionsRectTransform; //Recttransform of empty tower options object
    [SerializeField]
    private RectTransform standardTowerOptionsRectTransform; //Recttransform of standard tower options object
    [SerializeField]
    private RectTransform upgradedTowerOptionsRectTransform; //Recttransform of upgraded tower options object

    [SerializeField]
    private TextMeshProUGUI tmProGuiAttackTowerStats; //TMPro tower stats displaying element
    [SerializeField]
    private TextMeshProUGUI tmProGuiAoeTowerStats; //TMPro tower stats displaying element
    [SerializeField]
    private TextMeshProUGUI tmProGuiDebuffTowerStats; //TMPro tower stats displaying element
    [SerializeField]
    private TextMeshProUGUI tmProGuiUpgradeTowerStats; //TMPro tower stats displaying element
    [SerializeField]
    private TextMeshProUGUI tmProGuiSellTowerStats; //TMPro tower stats displaying element
    [SerializeField]
    private TextMeshProUGUI tmProGuiSellUpgradedTowerStats; //TMPro tower stats displaying element

    private Vector3 shopOptionsOriginalPostion; //Original position of shop element, before it moves to stay onscreen

    /// <summary>
    /// Concrete implementation of DisplayAttackTowerText. Displays string in TMPro element
    /// </summary>
    public override void DisplayAttackTowerText(string pText)
    {
        tmProGuiAttackTowerStats.SetText(pText);
    }

    /// <summary>
    /// Concrete implementation of DisplayAoeTowerText. Displays string in TMPro element
    /// </summary>
    public override void DisplayAoeTowerText(string pText)
    {
        tmProGuiAoeTowerStats.SetText(pText);
    }

    /// <summary>
    /// Concrete implementation of DisplayDebuffTowerText. Displays string in TMPro element
    /// </summary>
    public override void DisplayDebuffTowerText(string pText)
    {
        tmProGuiDebuffTowerStats.SetText(pText);
    }

    /// <summary>
    /// Concrete implementation of DisplayUpgradeTowerText. Displays string in TMPro element
    /// </summary>
    public override void DisplayUpgradeTowerText(string pText)
    {
        tmProGuiUpgradeTowerStats.SetText(pText);
    }

    /// <summary>
    /// Concrete implementation of DisplaySellTowerText. Displays string in TMPro element
    /// </summary>
    public override void DisplaySellTowerText(string pText)
    {
        tmProGuiSellTowerStats.SetText(pText);
    }

    /// <summary>
    /// Concrete implementation of DisplaySellUpgradedTowerText. Displays string in TMPro element
    /// </summary>
    public override void DisplaySellUpgradedTowerText(string pText)
    {
        tmProGuiSellUpgradedTowerStats.SetText(pText);
    }

    /// <summary>
    /// Concrete implementation of DisplayEmptyShopOption. Displays the empty shop options element, hides all other shop options
    /// </summary>
    public override void DisplayEmptyShopOption(Vector3 pScreenPos)
    {
        HideAllShopOptions();
        emptyTowerOptionsRectTransform.gameObject.SetActive(true);
        shopOptionsOriginalPostion = new Vector3(pScreenPos.x, pScreenPos.y - emptyTowerOptionsRectTransform.rect.height / 2, 0);
        ClampToWindow(pScreenPos, emptyTowerOptionsRectTransform, canvasRectTransform);
    }

    /// <summary>
    /// Concrete implementation of DisplayEmptyShopOption. Displays the standard shop options element, hides all other shop options
    /// </summary>
    public override void DisplayStandardShopOption(Vector3 pScreenPos)
    {
        HideAllShopOptions();
        standardTowerOptionsRectTransform.gameObject.SetActive(true);
        shopOptionsOriginalPostion = new Vector3(pScreenPos.x, pScreenPos.y - emptyTowerOptionsRectTransform.rect.height / 2, 0);
        ClampToWindow(pScreenPos, standardTowerOptionsRectTransform, canvasRectTransform);
    }

    /// <summary>
    /// Concrete implementation of DisplayEmptyShopOption. Displays the upgraded shop options element, hides all other shop options
    /// </summary>
    public override void DisplayUpgradedShopOption(Vector3 pScreenPos)
    {
        HideAllShopOptions();
        upgradedTowerOptionsRectTransform.gameObject.SetActive(true);
        shopOptionsOriginalPostion = new Vector3(pScreenPos.x, pScreenPos.y - emptyTowerOptionsRectTransform.rect.height / 2, 0);
        ClampToWindow(pScreenPos, upgradedTowerOptionsRectTransform, canvasRectTransform);
    }

    /// <summary>
    /// Hides all shop option elements
    /// </summary>
    public override void HideAllShopOptions()
    {
        emptyTowerOptionsRectTransform.gameObject.SetActive(false);
        standardTowerOptionsRectTransform.gameObject.SetActive(false);
        upgradedTowerOptionsRectTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        HideShopOptionsOnMouseInput();
    }

    /// <summary>
    /// Hides a shop option element, if the mouse is clicked a specific distance away from the element
    /// </summary>
    private void HideShopOptionsOnMouseInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (emptyTowerOptionsRectTransform.gameObject.activeSelf && (Input.mousePosition - shopOptionsOriginalPostion).magnitude > distanceToHideElement)
            {
                emptyTowerOptionsRectTransform.gameObject.SetActive(false);
            }
            if (standardTowerOptionsRectTransform.gameObject.activeSelf && (Input.mousePosition - shopOptionsOriginalPostion).magnitude > distanceToHideElement)
            {
                standardTowerOptionsRectTransform.gameObject.SetActive(false);
            }
            if (upgradedTowerOptionsRectTransform.gameObject.activeSelf && (Input.mousePosition - shopOptionsOriginalPostion).magnitude > distanceToHideElement)
            {
                upgradedTowerOptionsRectTransform.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// If a shop element would partially be displayed outside of the screen, move it back until fully inside
    /// </summary>
    private void ClampToWindow(Vector3 pScreenPos, RectTransform pPanelRectTransform, RectTransform pParentRectTransform)
    {

        pPanelRectTransform.transform.position = new Vector3(pScreenPos.x, pScreenPos.y - emptyTowerOptionsRectTransform.rect.height / 2, 0);

        Vector3 pos = pPanelRectTransform.localPosition;

        Vector3 minPosition = pParentRectTransform.rect.min - pPanelRectTransform.rect.min;
        Vector3 maxPosition = pParentRectTransform.rect.max - pPanelRectTransform.rect.max;

        pos.x = Mathf.Clamp(pPanelRectTransform.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(pPanelRectTransform.localPosition.y, minPosition.y, maxPosition.y);

        pPanelRectTransform.localPosition = pos;
    }
}
