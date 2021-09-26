using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorLever : MonoBehaviour
{

    public GameObject wall;
    public AudioClip audioClip;
    private Animator switchAnimator;
    private Animator wallAnimator;
    private Collider currentCollider;

    // Start is called before the first frame update
    void Start()
    {
        switchAnimator = GetComponent<Animator>();
        wall = GameObject.Find("SecretDoorWall");
        wallAnimator = wall.GetComponent<Animator>();
        wallAnimator.SetBool("open", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCollider != null)
        {
            if (currentCollider.CompareTag("Player"))
                //Animator curAnimator = currentCollider.gameObject.GetComponent<Animator>().GetComponent<Pu;
                if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                {
                    switchAnimator.SetBool("PlaySwitch", true);
                    wallAnimator.SetBool("open", true);
                    AudioSource.PlayClipAtPoint(audioClip, wall.transform.position);
                }

        }
    }

    private void OnTriggerEnter(Collider c)
    {
        currentCollider = c;

    }

    private void OnTriggerExit(Collider c)
    {
        currentCollider = null;
        if (c.CompareTag("Player"))
        {
        }
    }


}
