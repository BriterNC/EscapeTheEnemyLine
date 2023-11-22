using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float travelSpeed, destroyDelay;
    public bool useGravity, updateTravel, useVelocity, isParry, isHit;
    public GameObject particle;

    private Rigidbody _rig;
    
    void Start()
    {
        Destroy(gameObject, destroyDelay);
        _rig = GetComponent<Rigidbody>();
        _rig.useGravity = useGravity;
        if (!updateTravel)
        {
            _rig.velocity = transform.forward * travelSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") | other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("Hit Player!");
        }
        
        //Debug.Log("Hit! " + other.gameObject.name);
        
        // Create Particles on hitting
        if (!isHit)
        {
            ContactPoint contact = other.contacts[0];
            Instantiate(particle, contact.point, Quaternion.identity);
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            isHit = true;
        }
    }

    public bool IsUpdatingTravel()
    {
        return updateTravel;
    }
}
