using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private checkpointTracker tracker;
    private void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("Tracker").GetComponent<checkpointTracker>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tracker.lastCheckpointPassed = transform.position;
            tracker.lastRotation = other.transform.eulerAngles;
            if (this.CompareTag("room1"))
            {
                tracker.openedWalls[0] = true;
            }
            if (this.CompareTag("room2"))
            {
                tracker.openedWalls[1] = true;
            }
            if (this.CompareTag("room3"))
            {
                tracker.openedWalls[2] = true;
            }
            if (this.CompareTag("room4"))
            {
                tracker.openedWalls[3] = true;
            }
            if (this.CompareTag("room0"))
            {
                tracker.doorsOpened = true;
            }
            Destroy(this);
        }
    }
}
