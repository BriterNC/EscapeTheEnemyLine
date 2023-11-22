using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRayInteractorForceGrabFix : MonoBehaviour
{
    private XRRayInteractor xrRay;

    private void Start()
    {
        xrRay = GetComponent<XRRayInteractor>();
    }

    private void Update()
    {
        RaycastHit hit;
        bool didRayHit = xrRay.TryGetCurrent3DRaycastHit(out hit);
        //ray.GetCurrentRaycastHit(out hit);

        if (didRayHit && hit.transform.gameObject.CompareTag("Saber"))
        {
            xrRay.useForceGrab = true;
            //Debug.Log("Saber!!!");
        }
        else
        {
            xrRay.useForceGrab = false;
        }
        
        //Debug.Log(hit.transform.gameObject.name);
    }
}
