using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroPosition : MonoBehaviour
{
    private checkpointTracker tracker;
    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("Tracker").GetComponent<checkpointTracker>();
        transform.position = tracker.lastCheckpointPassed;
        transform.eulerAngles = tracker.lastRotation;
    }
}
