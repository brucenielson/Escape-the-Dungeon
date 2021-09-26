using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPath : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "MovingGround")
        {
            transform.parent = other.transform;
            Debug.Log("We are on a moving platform");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "MovingGround")
        {
            transform.parent = null;
        }
    }
}

