using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//John: When collision occurs call sound
public class CallPlaySoundOnCollision : MonoBehaviour
{

    public float magnitude;
    public GameObject myObject;
    private bool soundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.impulse.magnitude > magnitude && !soundPlayed)
        {
            soundPlayed = true;
            myObject.GetComponent<SoundOnAction>().PlaySound();
        }
    }
}
