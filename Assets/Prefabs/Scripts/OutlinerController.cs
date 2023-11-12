using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OutlinerAffordanceController : MonoBehaviour
{
    public Material outlinerMaterial;

    public void Start()
    {
        SetOutlineScale(false);
    }

    public void SetOutlineScale(bool trueFalse)
    {
        if (trueFalse)
        {
            outlinerMaterial.SetFloat("_Alpha", 1);
        }
        else if (!trueFalse)
        {
            outlinerMaterial.SetFloat("_Alpha", 0);
        }
    }

    /*private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Showing!");
            SetOutlineScale(true);
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            Debug.Log("Hiding!");
            SetOutlineScale(false);
        }
    }*/
}
