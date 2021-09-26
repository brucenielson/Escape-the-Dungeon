using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ProximityLightsEvent : MonoBehaviour
{
    //Right Point Lights
    public GameObject[] RightLights;
    public GameObject[] LeftLights;
    //Right Flames
    public GameObject[] RightFlames;
    public GameObject[] LeftFlames;
    private Stopwatch stopWatch;

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            stopWatch.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayLights();
    }

    private void PlayLights()
    {

        if (stopWatch.Elapsed.TotalMilliseconds > 1)
        {
            RightLights[0].GetComponent<Light>().enabled = true;
            RightFlames[0].SetActive(true);
            LeftLights[0].GetComponent<Light>().enabled = true;
            LeftFlames[0].SetActive(true);
        }
        if (stopWatch.Elapsed.TotalMilliseconds > 200)
        {
            RightLights[1].GetComponent<Light>().enabled = true;
            RightFlames[1].SetActive(true);
            LeftLights[1].GetComponent<Light>().enabled = true;
            LeftFlames[1].SetActive(true);
        }
        if (stopWatch.Elapsed.TotalMilliseconds > 400)
        {
            RightLights[2].GetComponent<Light>().enabled = true;
            RightFlames[2].SetActive(true);
            LeftLights[2].GetComponent<Light>().enabled = true;
            LeftFlames[2].SetActive(true);
        }
        if (stopWatch.Elapsed.TotalMilliseconds > 600)
        {
            RightLights[3].GetComponent<Light>().enabled = true;
            RightFlames[3].SetActive(true);
            LeftLights[3].GetComponent<Light>().enabled = true;
            LeftFlames[3].SetActive(true);
        }
        if (stopWatch.Elapsed.TotalMilliseconds > 800)
        {
            RightLights[4].GetComponent<Light>().enabled = true;
            RightFlames[4].SetActive(true);
            LeftLights[4].GetComponent<Light>().enabled = true;
            LeftFlames[4].SetActive(true);
        }
        //if (stopWatch.Elapsed.TotalMilliseconds > 1000)
        //{
        //    RightLights[5].GetComponent<Light>().enabled = true;
        //    RightFlames[5].SetActive(true);
        //    LeftLights[5].GetComponent<Light>().enabled = true;
        //    LeftFlames[5].SetActive(true);
        //}
        //if (stopWatch.Elapsed.TotalMilliseconds > 1200)
        //{
        //    RightLights[6].GetComponent<Light>().enabled = true;
        //    RightFlames[6].SetActive(true);
        //    LeftLights[6].GetComponent<Light>().enabled = true;
        //    LeftFlames[6].SetActive(true);
        //}
        //if (stopWatch.Elapsed.TotalMilliseconds > 1400)
        //{
        //    RightLights[7].GetComponent<Light>().enabled = true;
        //    RightFlames[7].SetActive(true);
        //    LeftLights[7].GetComponent<Light>().enabled = true;
        //    LeftFlames[7].SetActive(true);
        //}
    }
}
