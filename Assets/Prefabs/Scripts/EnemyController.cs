using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameController _gameController;
    public Transform player;
    public Transform[] destination;
    private Transform _tempDestination;
    private NavMeshAgent _agent;
    private float _stopDistance;
    public bool enteredShootingArea;
    private int _currentDestination = 0;
    public int bossHp = 5;
    public bool isBoss;
    private Vector3 startPosition;

    public void OnEnable()
    {
        startPosition = gameObject.transform.position;
    }

    void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _stopDistance = _agent.stoppingDistance;

        var temp = 0;
        foreach (Transform destinationTransform in destination)
        {
            destination[temp] = destinationTransform;
            temp++;
        }
        
        _tempDestination = destination[0];
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
        if (_gameController.gameOver)
        {
            return;
        }
        
        if (isBoss && bossHp <= 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            Debug.Log("Enemy Boss Died");
        }
        
        if (!enteredShootingArea)
        {
            _agent.stoppingDistance = 0;
        }
        else if (enteredShootingArea)
        {
            _agent.stoppingDistance = _stopDistance;
            destination[0] = player;
            if (transform.childCount > 1)
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
            if (!isBoss)
            {
                gameObject.transform.GetChild(0).GetComponent<BulletLauncher>().activated = false;
                _agent.stoppingDistance = _stopDistance;
                gameObject.SetActive(false);
                //Destroy(gameObject);
                Debug.Log("Enemy Died");
            }
            else if (isBoss)
            {
                bossHp--;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy Shooting Entry"))
        {
            enteredShootingArea = true;
        }
    }

    public void Respawn()
    {
        /*int temp = 0;*/
        destination[0] = _tempDestination;
        /*foreach (var VARIABLE in _tempDestination)
        {
        }*/
        
        if (isBoss)
        {
            transform.GetChild(0).GetComponent<BulletLauncher>().activated = false;
            transform.GetChild(1).GetComponent<BulletLauncher>().activated = false;
            bossHp = 5;
        }
        else if (!isBoss)
        {
            transform.GetChild(0).GetComponent<BulletLauncher>().activated = false;
        }
        
        enteredShootingArea = false;
        transform.position = startPosition;
        gameObject.SetActive(true);
    }
}
