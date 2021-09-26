using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class ExitEvent : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    //public GameObject durationTitle;
    //public GameObject eventStart;
    public AudioClip audioClip;
    public float volume;
    private AudioSource audioSource;
  
    public string timeElapsed;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            canvasGroup.interactable = true; 
            canvasGroup.blocksRaycasts = true; 
            canvasGroup.alpha = 1f;
            Time.timeScale = 0f;
            playSound();
            //timeElapsed = eventStart.GetComponent<StartTimerEvent>().getTimeElapsed().ToString(@"hh\:mm\:ss");
            //durationTitle.GetComponent<Text>().text = "Completion Time: " + timeElapsed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playSound()
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = false;
        audioSource.Play();
    }

 
 
}
