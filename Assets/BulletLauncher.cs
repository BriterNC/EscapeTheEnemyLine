using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletLauncher : MonoBehaviour
{
    public bool activated;
    private GameObject _player;
    public Vector2 intervalBetween;
    private float _countdownTime;
    private int _targetCount;
    public GameObject bullet;
    public Transform spawnPoint;
    private Transform _target;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _targetCount = _player.transform.GetChild(0).GetChild(5).childCount;
    }

    private void Update()
    {
        if (activated)
        {
            transform.LookAt(_player.transform.GetChild(0).GetChild(0));
            ShootPlayer();
        }
    }

    public void ShootPlayer()
    {
        int rand = Random.Range(0, _targetCount);
        _target = _player.transform.GetChild(0).GetChild(5).GetChild(rand);
        
        _countdownTime -= Time.deltaTime;
        
        if (_countdownTime > 0) return;

        _countdownTime = Random.Range(intervalBetween.x, intervalBetween.y);

        transform.LookAt(_target);
        Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
    }
}
