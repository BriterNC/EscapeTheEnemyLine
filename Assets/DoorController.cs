using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void OnCollisionExit(Collision other)
    {
        Debug.Log(other.gameObject.name + "was CollisionExit");
        /*if (other.gameObject.CompareTag("Enemy"))
        {
            transform.GetComponentInParent<Animator>().SetBool("character_nearby", false);
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + "was TriggerExit");
    }
}
