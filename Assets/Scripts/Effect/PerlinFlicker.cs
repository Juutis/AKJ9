// Date   : 23.02.2019 15:18
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

// from http://answers.unity3d.com/questions/41931/how-to-randomly-change-the-intensity-of-a-point-li.html
public class PerlinFlicker : MonoBehaviour
{

    [SerializeField]
    [Range(0f, 50f)]
    private float minIntensity = 0.25f;

    [SerializeField]
    [Range(1f, 50f)]
    private float maxIntensity = 1f;

    [SerializeField]
    [Range(0.1f, 20f)]
    private float speed = 5f;

    private float random;
    private Light lightObject;

    void Start()
    {
        lightObject = GetComponent<Light>();
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time * speed);
        lightObject.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }

}

