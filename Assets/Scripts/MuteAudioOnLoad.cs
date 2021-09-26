using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudioOnLoad : MonoBehaviour
{
    public float muteSeconds;
    // Start is called before the first frame update
    private DateTime timeToUnmute;
    void Start()
    {
        timeToUnmute = DateTime.Now.AddSeconds(muteSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now >= timeToUnmute)
        {
            GetComponent<AudioListener>().enabled = true;
            GetComponent<MuteAudioOnLoad>().enabled = false;
        }
    }
}
