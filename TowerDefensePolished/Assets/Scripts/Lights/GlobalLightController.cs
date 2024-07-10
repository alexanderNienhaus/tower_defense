using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    [SerializeField]
    private ShopOpenController shopOpenController;
    [SerializeField]
    private NightArivedEvent nightArivedEvent;
    [SerializeField]
    private DayArivedEvent dayArivedEvent;
    [SerializeField]
    private float nightIntensity = 0.01f;
    [SerializeField]
    private float dayIntensity = 1f;
    [SerializeField]
    private float transitionTime = 3f;
    [SerializeField]
    private float transitionSteps = 0.01f;

    Dictionary<Vector3, Dictionary<Light2D, float>> towerLights;
    private Volume globalVolume;
    private Light2D globalLight;
    private float globalVolumeWeight;

    public void OnShoptimeOver()
    {
        StartCoroutine(DimLightOverTime(towerLights, transitionTime, false));
    }

    public void OnWaveOver()
    {
        StartCoroutine(DimLightOverTime(towerLights, transitionTime, true));
    }

    public void OnTowerChanged(Vector3 pPosition, TowerController pTower, ShopAction pShopAction)
    {
        switch (pShopAction)
        {
            case ShopAction.Buy:
                towerLights[pPosition] = new Dictionary<Light2D, float>();
                foreach (Light2D light2D in pTower.GetComponentsInChildren<Light2D>())
                {
                    towerLights[pPosition].Add(light2D, light2D.intensity);
                    light2D.intensity = 0;
                }
                break;
            case ShopAction.Upgrade:
                towerLights.Remove(pPosition);
                towerLights[pPosition] = new Dictionary<Light2D, float>();
                foreach (Light2D light2D in pTower.GetComponentsInChildren<Light2D>())
                {
                    towerLights[pPosition].Add(light2D, light2D.intensity);
                    light2D.intensity = 0;
                }
                break;
            case ShopAction.Sell:
                towerLights.Remove(pPosition);
                break;
        }
    }

    private void Start()
    {
        globalLight = GetComponent<Light2D>();
        if (globalLight == null)
        {
            throw new Exception("There is no Light2D component.");
        }

        globalVolume = GetComponent<Volume>();
        if (globalVolume == null)
        {
            throw new Exception("There is no Volume component.");
        }
        globalVolumeWeight = globalVolume.weight;
        globalVolume.weight = 0;

        towerLights = new Dictionary<Vector3, Dictionary<Light2D, float>>();
    }

    private IEnumerator DimLightOverTime(Dictionary<Vector3, Dictionary<Light2D, float>> pTowerLights, float pDuration, bool pToDaytime)
    {
        float counter = 0;
        while (counter < pDuration)
        {
            counter += Time.deltaTime;

            //Lerp tower lights
            for (int i = 0; i < pTowerLights.Count; i++)
            {
                for (int j = 0; j < pTowerLights.ElementAt(i).Value.Count; j++)
                {
                    pTowerLights.ElementAt(i).Value.ElementAt(j).Key.intensity =
                        Mathf.Lerp(pToDaytime ? pTowerLights.ElementAt(i).Value.ElementAt(j).Value : 0,
                        pToDaytime ? 0 : pTowerLights.ElementAt(i).Value.ElementAt(j).Value, counter / pDuration);
                }
            }

            //Lerp global light
            globalLight.intensity =Mathf.Lerp(pToDaytime ? nightIntensity : dayIntensity, pToDaytime ? dayIntensity : nightIntensity, counter / pDuration);
            if (globalLight.intensity == nightIntensity)
            {
                nightArivedEvent.Raise();
            } else if (globalLight.intensity == dayIntensity)
            {
                dayArivedEvent.Raise();
            }

            //Lerp global bloom
            globalVolume.weight = Mathf.Lerp(pToDaytime ? globalVolumeWeight : 0, pToDaytime ? 0 : globalVolumeWeight, counter / pDuration);

            yield return null;
        }
    }
}