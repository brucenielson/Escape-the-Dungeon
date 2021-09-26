using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class StartScreamEvent : MonoBehaviour
{

    private bool playedScream = false;
    public GameObject ObjectScream;

    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            if (!playedScream)
            {
                ObjectScream.GetComponent<PlaySoundFXTimer>().PlaySoundTimer();
                playedScream = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
}
