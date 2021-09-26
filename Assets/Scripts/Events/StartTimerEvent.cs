using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class StartTimerEvent : MonoBehaviour
{

    private bool timerStarted = false;
    private Stopwatch stopWatch;

    // Start is called before the first frame update
    void Start()
    {
        stopWatch = new Stopwatch();
        //stopWatch.Stop();
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if (!timerStarted)
            {
                stopWatch.Start();
                timerStarted = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public System.TimeSpan getTimeElapsed()
    {
        return stopWatch.Elapsed;
    }

}
