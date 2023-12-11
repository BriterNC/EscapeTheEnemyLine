using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float travelSpeed, destroyDelay;
    public bool useGravity, updateTravel, useVelocity, isParry, isHit;
    public GameObject particle, gameController;
    public AudioSource hitSaberSound;

    private Rigidbody _rig;
    
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
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
        // Create Particles on hitting
        if (!isHit)
        {
            ContactPoint contact = other.contacts[0];
            Instantiate(particle, contact.point, Quaternion.identity);
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            isHit = true;
            hitSaberSound.Play();
        }

        if (other.gameObject.CompareTag("Saber"))
        {
            isParry = true;
            return;
        }

        if (isParry)
        {
            return;
        }
        
        if (!isParry && (other.gameObject.CompareTag("Player") | other.gameObject.CompareTag("MainCamera")))
        {
            //Debug.Log("Hit Player!");
            gameController.GetComponent<GameController>().Damage();
        }
        
        Debug.Log("Hit! " + other.gameObject.name);
    }

    public bool IsUpdatingTravel()
    {
        return updateTravel;
    }
}
