using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberController : MonoBehaviour
{
    public Animator saberAnimator;
    public bool saberIsOn = false;
    public AudioSource turnOnSound, turnOffSound;

    public void TurnSaberOnOff()
    {
        if (!saberIsOn)
        {
            saberAnimator.SetTrigger("TurnOn");
            turnOnSound.Play();
            saberIsOn = true;
        }
        else if (saberIsOn)
        {
            saberAnimator.SetTrigger("TurnOff");
            turnOffSound.Play();
            saberIsOn = false;
        }
    }
    
    public void TurnSaberOnOff(string onOff)
    {
        switch (onOff)
        {
            case ("On"):
                if (!saberIsOn)
                {
                    saberAnimator.SetTrigger("TurnOn");
                    turnOnSound.Play();
                }
                saberIsOn = true;
                break;
            case ("Off"):
                if (saberIsOn)
                {
                    saberAnimator.SetTrigger("TurnOff");
                    turnOffSound.Play();
                }
                saberIsOn = false;
                break;
        }
    }
}
