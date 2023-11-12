using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableAttachFix : XRGrabInteractable
{
    public Transform leftAttachPoint;
    public Transform rightAttachPoint;
    
    public override Transform GetAttachTransform(IXRInteractor interactor)
    {
        //Debug.Log("GetAttachTransform");

        Transform i_attachTransform = null ;

        if (interactor.transform.CompareTag("Left Hand"))
        {
            //Debug.Log("Left") ;
            i_attachTransform  = leftAttachPoint ;
        }
        
        if (interactor.transform.CompareTag("Right Hand"))
        {
            //Debug.Log("Right") ;
            i_attachTransform  = rightAttachPoint ;
        }
        
        return i_attachTransform != null ? i_attachTransform : base.GetAttachTransform(interactor);
    }
}
