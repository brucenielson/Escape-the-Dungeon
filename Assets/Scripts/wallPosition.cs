using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallPosition : MonoBehaviour
{
    private checkpointTracker tracker;
    void Start()
    {
        tracker = GameObject.FindGameObjectWithTag("Tracker").GetComponent<checkpointTracker>();

        if (this.CompareTag("Wall1"))
        {
            if (tracker.openedWalls[0])
            {
                this.GetComponent<Animator>().SetBool("open", true);
            }
        }

        if (this.CompareTag("Wall2"))
        {
            if (tracker.openedWalls[1])
            {
                this.GetComponent<Animator>().SetBool("open", true);
            }
        }

        if (this.CompareTag("Wall3"))
        {
            if (tracker.openedWalls[2])
            {
                this.GetComponent<Animator>().SetBool("open", true);
            }
        }

        if (this.CompareTag("Wall4"))
        {
            if (tracker.openedWalls[3])
            {
                this.GetComponent<Animator>().SetBool("open", true);
            }
        }

    }
}
