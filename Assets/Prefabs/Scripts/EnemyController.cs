using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Transform[] destination;
    private NavMeshAgent _agent;
    private float _stopDistance;
    public bool enteredShootingArea;
    private int _currentDestination = 0;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _stopDistance = _agent.stoppingDistance;
        if (destination.Length > 0)
        {
            _currentDestination = destination.Length - 1;
            //Debug.Log("Total Destination = " + destination.Length);
            _agent.SetDestination(destination[_currentDestination].position);
            //Debug.Log("Current Destination = " + _currentDestination);
        }
        else if (destination.Length == 0)
        {
            destination = new Transform[1];
            destination[0] = player;
        }
    }
    
    void Update()
    {
        if (!enteredShootingArea)
        {
            _agent.stoppingDistance = 0;
        }
        else if (enteredShootingArea)
        {
            _agent.stoppingDistance = _stopDistance;
            destination[0] = player;
            if (transform.GetChildCount() > 1)
            {
                transform.GetChild(0).GetComponent<BulletLauncher>().activated = true;
                transform.GetChild(1).GetComponent<BulletLauncher>().activated = true;
            }
            else
            {
                transform.GetChild(0).GetComponent<BulletLauncher>().activated = true;
            }
            transform.LookAt(new Vector3(destination[_currentDestination].position.x, transform.position.y, destination[_currentDestination].position.z));
        }
        _agent.SetDestination(destination[_currentDestination].position);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination") && other.gameObject == destination[_currentDestination].gameObject)
        {
            if (_currentDestination != 0)
            {
                _currentDestination--;
            }
            else
            {
                destination[0] = player;
            }
            //Debug.Log("Current Destination = " + _currentDestination);
        }

        if (other.gameObject.CompareTag("Bullet") && other.gameObject.GetComponent<Bullet>().isParry)
        {
            Destroy(gameObject);
            Debug.Log("Bye Bye Enemy");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy Shooting Entry"))
        {
            enteredShootingArea = true;
        }
    }
}
