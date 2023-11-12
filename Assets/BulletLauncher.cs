using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletLauncher : MonoBehaviour
{
    private GameObject _player;
    public float timer = 5;
    public float countdownTime;
    public int targetCount;
    public GameObject bullet;
    public Transform spawnPoint;
    public Transform target;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        targetCount = _player.transform.GetChild(0).GetChild(5).childCount;
    }

    private void Update()
    {
        transform.LookAt(_player.transform.GetChild(0).GetChild(0));
        ShootPlayer();
    }

    public void ShootPlayer()
    {
        int rand = Random.Range(0, targetCount);
        target = _player.transform.GetChild(0).GetChild(5).GetChild(rand);
        
        countdownTime -= Time.deltaTime;
        
        if (countdownTime > 0) return;

        countdownTime = timer;

        transform.LookAt(target);
        Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    }
}
