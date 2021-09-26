using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject wall;
    public AudioClip audioClip;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = wall.GetComponent<Animator>();        
    }

    public void Open()
    {
        anim.SetBool("open", true);
        AudioSource.PlayClipAtPoint(audioClip, wall.transform.position);
    }

    public void Close()
    {
        anim.SetBool("open", false);
    }

}
