using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLighting : MonoBehaviour
{
    Light myLight;
    public float minIntensity;
    public float maxIntensity;
    public float changeSpeed;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        myLight.intensity =
            minIntensity + Mathf.PingPong(Time.time, maxIntensity - minIntensity);
    }
}
