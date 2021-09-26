using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Video;

public class Fade : MonoBehaviour
{
    public float fadeAfterSeconds;
    public float fadeSpeed;
    private Renderer rend;

    private DateTime timeToFade;
    private bool doneFading;
    private bool fadeIn;
    private bool fadeOut;

    void Start()
    {
        rend = this.GetComponent<Renderer>();
        timeToFade = DateTime.Now.AddSeconds(fadeAfterSeconds);
        TriggerFadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (!doneFading)
        {
            if (fadeIn)
            {
                FadeIn();
            }
            if (fadeOut)
            {
                FadeOut();
            }
        }

    }

    private void FadeIn()
    {
        if (DateTime.Now >= timeToFade)
        {
            var color = rend.material.color;

            var newValue = color.a + (Time.deltaTime * fadeSpeed);
            if (newValue >= 255)
            {
                newValue = 255;
                doneFading = true;
                fadeIn = false;
            }
            else
            {
                var newColor = new Color(color.r, color.g, color.b, newValue);
                rend.material.color = newColor;
            }
        }
    }

    private void FadeOut()
    {
        if (DateTime.Now >= timeToFade)
        {
            var color = rend.material.color;

            var newValue = color.a - (Time.deltaTime * fadeSpeed);
            if (newValue <= 0)
            {
                newValue = 0;
                doneFading = true;
                fadeOut = false;
            }
            else
            {
                var newColor = new Color(color.r, color.g, color.b, newValue);
                rend.material.color = newColor;
            }
        }
    }

    public void TriggerFadeIn()
    {
        fadeIn = true;
        doneFading = false;
    }

    public void TriggerFadeOut()
    {
        fadeOut = true;
        doneFading = false;
    }

}

