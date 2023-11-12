using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

enum ParryMethod
{
    Inverse,
    Reflect,
    Aim
}

public class Parry : MonoBehaviour
{
    private ParryMethod parryMethod;
    public bool adjustBulletPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.GetComponent<Bullet>().isParry)
            {
                return;
            }

            other.GetComponent<Bullet>().isParry = true;
            Vector3 direction = ((other.transform.position + other.transform.forward) - other.transform.position).normalized;
            Vector3 inverse = direction * -1;
            Vector3 position = other.transform.position;

            switch (parryMethod)
            {
                case (ParryMethod.Inverse):
                    //Debug.DrawRay(position, inverse, Color.magenta, 100f);
                    other.transform.rotation = Quaternion.LookRotation(inverse);
                    if (!other.GetComponent<Bullet>().IsUpdatingTravel())
                    {
                        other.GetComponent<Rigidbody>().velocity *= -1;
                    }
                    break;
                case (ParryMethod.Reflect):
                    //Debug.DrawRay(position, Vector3.Reflect(direction, transform.forward), Color.magenta, 100f);
                    Vector3 reflected = Vector3.Reflect(direction, transform.forward);
                    other.transform.rotation = Quaternion.LookRotation(reflected);
                    float mag = other.transform.GetComponent<Rigidbody>().velocity.magnitude;
                    if (!other.GetComponent<Bullet>().IsUpdatingTravel())
                    {
                        other.GetComponent<Rigidbody>().velocity = reflected.normalized * mag;
                    }
                    break;
                case (ParryMethod.Aim):
                    Vector3 aimDirection = Camera.main.transform.forward;
                    other.transform.rotation = Quaternion.LookRotation(aimDirection);
                    float mag1 = other.transform.GetComponent<Rigidbody>().velocity.magnitude;
                    if (!other.GetComponent<Bullet>().IsUpdatingTravel())
                    {
                        other.GetComponent<Rigidbody>().velocity = aimDirection * mag1;
                    }
                    if (adjustBulletPosition)
                    {
                        other.transform.position = transform.position;
                        //Debug.DrawRay(other.transform.position, aimDirection, Color.magenta, 100f);
                    }
                    else
                    {
                        //Debug.DrawRay(position, aimDirection, Color.magenta, 100f);
                    }
                    break;
            }
        }
    }
}
