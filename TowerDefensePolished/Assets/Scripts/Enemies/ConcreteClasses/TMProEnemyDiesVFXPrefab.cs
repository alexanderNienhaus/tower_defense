using UnityEngine;
using TMPro;

public class TMProEnemyDiesVFXPrefab : AbstractEnemyDiesVFXPrefab
{
    private TextMeshProUGUI tmProEnemyDiesVFXText; //TMPro wave displaying element
    private float timeToLive; //Time until vfx element gets destroyed

    /// <summary>
    /// Concrete implementation of DisplayText. Displays string in TMPro element, positions it correctly and sets the time to live
    /// </summary>
    public override void DisplayText(string pText, Vector3 pPosition, float pTimeToLive)
    {
        timeToLive = pTimeToLive;
        tmProEnemyDiesVFXText.rectTransform.position = Camera.main.WorldToScreenPoint(pPosition);
        tmProEnemyDiesVFXText.gameObject.SetActive(true);
        tmProEnemyDiesVFXText.SetText(pText);
    }

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initializes strategy
    /// </summary>
    private void Initialize()
    {
        tmProEnemyDiesVFXText = GetComponent<TextMeshProUGUI>();
        if (tmProEnemyDiesVFXText == null)
        {
            throw new System.Exception("There is no component that implements the TextMeshProUGUI abstract class.");
        }
    }

    private void FixedUpdate()
    {
        CountDownTimeToLive();
    }

    /// <summary>
    /// Counts down time to live, until the vfx element gets destroyed
    /// </summary>
    private void CountDownTimeToLive()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime >= timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
