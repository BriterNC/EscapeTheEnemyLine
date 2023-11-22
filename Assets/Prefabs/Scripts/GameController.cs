using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public int[] assignedEnemyInWave;
    
    public int totalWave;
    public int currentWave;
    public int remainEnemy;
    /*
    public int currentEnemy;
    public int spawnedEnemyFloor1;
    public int spawnedEnemyFloor2;
    public Transform[] spawnPointFloor1;
    public Transform[] spawnPointFloor2;
    public int enemyFloor1Limit; // Don't change this in inspector, it's ref from spawnPoint
    public int enemyFloor2Limit; // Don't change this in inspector, it's ref from spawnPoint
    public GameObject enemyGameObject;

    private Coroutine myCoroutine;
    */
    
    private void Start()
    {
        totalWave = assignedEnemyInWave.Length;
        //myCoroutine = StartCoroutine(UpdateGraph(totalWave));
        //remainEnemy = ((currentWave * currentWave) / 2) + (currentWave / 2); // Parabola upward

        //enemyFloor1Limit = spawnPointFloor1.Length;
        //enemyFloor2Limit = spawnPointFloor2.Length;
    }

    public void FindEnemyLimit()
    {
        if (currentWave == assignedEnemyInWave[currentWave - 1])
        {
            remainEnemy = assignedEnemyInWave[currentWave];
        }
    }
    
    /*
    1   =   1
    2   =   2
    3   =   3
    4   =   5
    5   =   7
    6   =   9
    7   =   12
    8   =   15
    8   =   15
    9   =   20
    10  =   25
    */

    /*private void Update()
    {
        if (spawnedEnemyFloor1 < enemyFloor1Limit)
        {
            SpawnEnemy(enemyGameObject, 1);
        }
        if (spawnedEnemyFloor2 < enemyFloor2Limit)
        {
            SpawnEnemy(enemyGameObject, 2);
        }
    }*/

    /*private void SpawnEnemy(GameObject enemy, int floor)
    {
        //int rand = Random.Range(0, spawnPoint.Length - 1);
        //Instantiate(enemy, spawnPoint[rand], Quaternion.identity);
    }*/
}
