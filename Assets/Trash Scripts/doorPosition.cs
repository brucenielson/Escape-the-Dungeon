using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorPosition : MonoBehaviour
{
    private checkpointTracker tracker;
    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("Tracker").GetComponent<checkpointTracker>();
        if (tracker.doorsOpened)
        {
            this.GetComponent<DoorPuzzle>().submit();
        }
    }
}
