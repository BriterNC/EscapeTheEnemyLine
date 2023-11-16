using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberController : MonoBehaviour
{
    public Animator saberAnimator;
    public bool saberIsOn = false;

    public void TurnSaberOnOff()
    {
        if (!saberIsOn)
        {
            saberAnimator.SetTrigger("TurnOn");
            saberIsOn = true;
        }
        else if (saberIsOn)
        {
            saberAnimator.SetTrigger("TurnOff");
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
                }
                saberIsOn = true;
                break;
            case ("Off"):
                if (saberIsOn)
                {
                    saberAnimator.SetTrigger("TurnOff");
                }
                saberIsOn = false;
                break;
        }
    }
}
