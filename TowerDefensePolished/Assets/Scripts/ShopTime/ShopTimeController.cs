using System.Collections;
using UnityEngine;

/// <summary>
/// Controls shop time, specifically the time model and the time displayer. Passes events onto them and raises events itself.
/// This controller starts the shop time at the start of the game or when a wave over event is pushed. It uses a coroutine to count
/// the shop time down to zero and pushes a shop time over event. The time model strategy calculates the shoptime and calls the
/// dispaly method of the shop time display strategy to display the shop time in the GUI. This class also listens to the shop open clicked event,
/// that is raised when the player wants to access the shop, the controller then checks if the shop time is active and raises a
/// right time or not right time event
/// </summary>
public class ShopTimeController : MonoBehaviour
{
    [SerializeField]
    RightTimeEvent rightTimeEvent; //Event for opening the shop during shop time
    [SerializeField]
    NotRightTimeEvent notRightTimeEvent; //Event for opening the shop during a wave
    [SerializeField]
    ShopTimeOverEvent shopTimeOverEvent; //Event for when the shop time ends and a wave should start

    private AbstractShopTimeModelStrategy shopTimeModelStrategy;  //Strategy for calculating shop time
    private IEnumerator shopTimeCoroutine; //Coroutine for counting down shop time
    private bool shopTimeActive = false; //Bool for activity of countdown

    public void OnDayArived()
    {
        shopTimeModelStrategy.GetSkipShopTimeButton().SetActive(true);
    }

    public void OnWaveOver()
    {
        StartTimer();
    }


    /// <summary>
    /// Listens to shop open clicked event. Raises either a right time or not the right time event, depending on acitivity of shop time
    /// </summary>
    public void OnShopOpenClicked(Vector3 pPosition, TowerController pTower)
    {
        if (shopTimeActive)
        {
            rightTimeEvent.Raise(pPosition, pTower);
        }
        else
        {
            notRightTimeEvent.Raise();
        }
    }

    /// <summary>
    /// Skips shop time and starts wave immediately. Updates GUI
    /// </summary>
    public void SkipShopTime()
    {
        StopCoroutine(shopTimeCoroutine);
        shopTimeActive = false;
        shopTimeModelStrategy.UpdateShopTime(true);
        shopTimeModelStrategy.GetSkipShopTimeButton().SetActive(false);
        shopTimeOverEvent.Raise();
    }

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Gets components, initializes strategy, starts timer for the first time
    /// </summary>
    private void Initialize()
    {
        AbstractShopTimeDisplayStrategy shopTimeDisplayStrategy = GetComponent<AbstractShopTimeDisplayStrategy>();
        if (shopTimeDisplayStrategy != null)
        {
            shopTimeModelStrategy = GetComponent<AbstractShopTimeModelStrategy>();
            if (shopTimeModelStrategy != null)
            {
                shopTimeModelStrategy.Initialize(shopTimeDisplayStrategy);
                StartTimer();
            }
            else
            {
                throw new System.Exception("There is no component that implements the AbstractTimeModel abstract class.");
            }
        }
        else
        {
            throw new System.Exception("There is no component that implements the AbstractTimeDisplayer abstract class.");
        }
    }

    /// <summary>
    /// Starts coroutine that counts down shop time
    /// </summary>
    private void StartTimer()
    {
        shopTimeActive = true;
        shopTimeCoroutine = TimeCoroutine();
        StartCoroutine(shopTimeCoroutine);
    }

    /// <summary>
    /// Coroutine that counts down shop time. Raises shop time over events if 0 is reached. Updates GUI
    /// </summary>
    private IEnumerator TimeCoroutine()
    {
        while (shopTimeActive)
        {
            shopTimeActive = shopTimeModelStrategy.UpdateShopTime();
            if (!shopTimeActive)
            {
                shopTimeModelStrategy.GetSkipShopTimeButton().SetActive(false);
                shopTimeOverEvent.Raise();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
