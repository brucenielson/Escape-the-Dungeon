using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPosition : MonoBehaviour
{
    // Expanded script to include audio
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // This script goes on the tablet itself and allows me to specify (and look up) the correction location and
    // rotation information to make the tablet look right in hand. Can be used with any holdable item, actually. 

    // Start is called before the first frame update
    public Vector3 localPosition = Vector3.zero;
    public Quaternion localRotation = Quaternion.identity;

    // Used to figure out what a manually set position would be if I set it via local position numbers
    //private void Update()
    //{
    //    Rigidbody rb = GetComponent<Rigidbody>();
    //    Debug.Log("Local Position:" + rb.transform.localPosition.ToString());
    //    Debug.Log("Local Rotation:" + rb.transform.localRotation.ToString());
    //}

    // Make a sound when you drop the item -- TODO: this doesn't really belong in this script -- should rename and repurpose script.
    private void OnCollisionEnter(Collision collision)
    {
        // Cite: https://asyncaudio.com/blogs/tutorials/how-to-create-a-simple-audio-impact-collision-trigger-with-unity
        // Cite: https://answers.unity.com/questions/261556/how-to-tell-if-my-character-hit-a-collider-of-a-ce.html
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Default") && collision.relativeVelocity.magnitude > 3f)
            audioSource.Play();
    }
}
