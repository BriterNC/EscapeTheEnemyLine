using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticInteractable : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity;
    public float duration;
    private GameObject _player;
    private XRBaseController _leftController, _rightController;
    public bool inLeftHand, inRightHand;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _leftController = _player.transform.GetChild(0).Find("Left Controller").GetComponent<XRBaseController>();
        _rightController = _player.transform.GetChild(0).Find("Right Controller").GetComponent<XRBaseController>();
    }

    public void TriggerHaptic()
    {
        if (inLeftHand)
        {
            //Debug.Log("Sending Haptic to Left Controller");
            _leftController.SendHapticImpulse(intensity, duration);
        }
        if (inRightHand)
        {
            //Debug.Log("Sending Haptic to Right Controller");
            _rightController.SendHapticImpulse(intensity, duration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Left Hand"))
        {
            inLeftHand = true;
        }
        if (other.gameObject.CompareTag("Right Hand"))
        {
            inRightHand = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Left Hand"))
        {
            inLeftHand = false;
        }
        if (other.gameObject.CompareTag("Right Hand"))
        {
            inRightHand = false;
        }
    }
}
