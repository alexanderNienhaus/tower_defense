using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField]
    private int framesPerRandomize = 60;
    [SerializeField]
    private float deviation = 0.1f;

    private Light2D light2D;
    private float standardIntensity;
    private int frames = 0;

    private void Awake()
    {
        light2D = GetComponentInChildren<Light2D>();
        if (light2D == null)
        {
            throw new Exception("There is no Light2D component.");
        }
        standardIntensity = light2D.intensity;
    }

    private void Update()
    {
        frames++;
        if (frames % framesPerRandomize == 0 && light2D.intensity > 0)
        {
            RandomizeIntensity();
        }
    }

    private void RandomizeIntensity()
    {
        System.Random random = new System.Random();
        float randomValue = (float)(random.NextDouble() * (standardIntensity + deviation - (standardIntensity - deviation)) + (standardIntensity - deviation));
        light2D.intensity = randomValue;
    }
}
