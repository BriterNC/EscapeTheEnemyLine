using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float travelSpeed, destroyDelay;
    public bool useGravity, updateTravel, useVelocity, isParry;
    public GameObject particle;

    private Rigidbody rig;
    
    void Start()
    {
        Destroy(gameObject, destroyDelay);
        rig = GetComponent<Rigidbody>();
        rig.useGravity = useGravity;
        if (!updateTravel)
        {
            rig.velocity = transform.forward * travelSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        /*if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player!");
        }*/
        Debug.Log("Hit! " + other.gameObject.name);

        ContactPoint contact = other.contacts[0];
        Instantiate(particle, contact.point, Quaternion.identity);
        
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsUpdatingTravel()
    {
        return updateTravel;
    }
}
