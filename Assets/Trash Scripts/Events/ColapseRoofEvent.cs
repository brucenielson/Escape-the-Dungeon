using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ColapseRoofEvent : MonoBehaviour
{
    public GameObject falseRoof;
    public GameObject brickSpeaker;
    public GameObject sphere;
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
            falseRoof.GetComponent<BoxCollider>().enabled = false;
            brickSpeaker.GetComponent<SoundOnAction>().PlaySound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void disableSphere()
    {
        stopWatch.Start();
        if (stopWatch.Elapsed.TotalSeconds > 1)
        {
            sphere.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
