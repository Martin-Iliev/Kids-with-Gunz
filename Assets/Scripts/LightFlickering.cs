using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public float flickerIntensity = 0.2f;
    public float flickersPerSecond = 3.0f;
    public float speedRandomness = 1.0f;

    private float time;
    private float startingIntensity;
    private Light fire;

    void Start()
    {
        fire = GetComponent<Light>();
        startingIntensity = fire.intensity;
    }

    void Update()
    {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        fire.intensity = startingIntensity + Mathf.Sin(time * flickersPerSecond) * flickerIntensity;
    }
}