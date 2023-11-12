using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float travelSpeed, destroyDelay;
    public bool useGravity, updateTravel, useVelocity, isParry;

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

    public bool IsUpdatingTravel()
    {
        return updateTravel;
    }
}
