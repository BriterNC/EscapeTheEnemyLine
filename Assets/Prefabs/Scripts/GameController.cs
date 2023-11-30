using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public int[] assignedEnemyInWave;
    
    private int totalWave;
    public int currentWave;

    public GameObject playerGameObject;
    public int playerHp, playerMp;

    public bool leftUsingMp, rightUsingMp;
    public float elapsedUsingMp, elapsedRegenMp;
    public TextMeshProUGUI playerHpTextMesh, playerMpTextMesh;

    public GameObject damagePanel;
    public bool gameOver;
    public GameObject gameOverPanel, gameOverText;

    public TextMeshProUGUI currentWaveTextMesh;
    public bool waveStarted;
    public GameObject startButton, alertSound;

    public GameObject[] door;
    public GameObject[] tempEnemySet;
    public GameObject[] led;
    public GameObject monitorPanel;
    public Material ledRed;

    public GameObject[] ledShipEntrance;
    public Material ledRedShipEntrance;
    
    public int currentEnemy;
    public int remainEnemy;
    public int spawnedEnemyFloor1;
    public int spawnedEnemyFloor2;
    public GameObject[] spawnPointFloor1;
    public GameObject[] spawnPointFloor2;
    public int enemyFloor1Limit; // Don't change this in inspector, it's ref from spawnPoint
    public int enemyFloor2Limit; // Don't change this in inspector, it's ref from spawnPoint
    public GameObject enemyGameObject;

    //private Coroutine myCoroutine;
    
    private void Start()
    {
        totalWave = assignedEnemyInWave.Length;
        
        //myCoroutine = StartCoroutine(UpdateGraph(totalWave));
        //remainEnemy = ((currentWave * currentWave) / 2) + (currentWave / 2); // Parabola upward
        
        enemyFloor1Limit = spawnPointFloor1.Length;
        enemyFloor2Limit = spawnPointFloor2.Length;
        
        //Temp
        StartWave();
    }

    public void StartWave()
    {
        foreach (GameObject entryDoor in door)
        {
            entryDoor.GetComponent<Animator>().SetBool("character_nearby", true);
        }
        foreach (GameObject enemy in tempEnemySet)
        {
            enemy.SetActive(true);
        }
        foreach (GameObject ledTemp in led)
        {
            ledTemp.GetComponent<MeshRenderer>().material = ledRed;
        }
        foreach (GameObject ledTemp in ledShipEntrance)
        {
            ledTemp.GetComponent<MeshRenderer>().material = ledRedShipEntrance;
        }
        monitorPanel.GetComponent<Image>().color = Color.red;
        
        currentWave = 1;
        waveStarted = true;
        startButton.SetActive(false);
        alertSound.SetActive(true);
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
    9   =   20
    10  =   25
    */

    private void Update()
    {
        playerHpTextMesh.text = playerHp.ToString();
        playerMpTextMesh.text = playerMp.ToString();

        if (gameOver)
        {
            return;
        }
        
        if (playerHp <= 0)
        {
            gameOverPanel.SetActive(true);
            gameOverText.SetActive(true);
            gameOver = true;
            
            // Continue
        }
        
        CheckCurrentWaveRemainEnemy();

        if (playerMp <= 0)
        {
            playerGameObject.transform.GetChild(0).GetChild(3).gameObject.GetComponent<XRRayInteractor>().enabled = false;
            playerGameObject.transform.GetChild(0).GetChild(4).gameObject.GetComponent<XRRayInteractor>().enabled = false;
        }
        else if (playerMp > 0)
        {
            playerGameObject.transform.GetChild(0).GetChild(3).gameObject.GetComponent<XRRayInteractor>().enabled = true;
            playerGameObject.transform.GetChild(0).GetChild(4).gameObject.GetComponent<XRRayInteractor>().enabled = true;
        }
        
        if (playerMp > 0)
        {
            if (leftUsingMp)
            {
                elapsedUsingMp += Time.deltaTime;
                if (elapsedUsingMp >= 0.1f)
                {
                    elapsedUsingMp = (elapsedUsingMp % 0.1f);
                    playerMp -= 1;
                }
            }
            if (rightUsingMp)
            {
                elapsedUsingMp += Time.deltaTime;
                if (elapsedUsingMp >= 0.1f)
                {
                    elapsedUsingMp = (elapsedUsingMp % 0.1f);
                    playerMp -= 1;
                }
            }
        }

        if (!leftUsingMp && !rightUsingMp && playerMp < 100)
        {
            elapsedRegenMp += Time.deltaTime;
            if (elapsedRegenMp >= 0.2f)
            {
                elapsedRegenMp = (elapsedRegenMp % 0.2f);
                playerMp += 2;
                if (playerMp > 100)
                {
                    playerMp = 100;
                }
            }
        }

        if (!waveStarted)
        {
            return;
        }
        
        currentWaveTextMesh.text = $"Wave: {currentWave} / {totalWave}";
        
        if (spawnedEnemyFloor1 < enemyFloor1Limit)
        {
            SpawnEnemy(enemyGameObject, 1);
        }
        if (spawnedEnemyFloor2 < enemyFloor2Limit)
        {
            SpawnEnemy(enemyGameObject, 2);
        }
    }

    private void SpawnEnemy(GameObject enemy, int floor)
    {
        if (currentEnemy >= remainEnemy)
        {
            return;
        }
        else
        {
            if (floor == 1)
            {
                int rand = Random.Range(0, spawnPointFloor1.Length - 1);
                Vector3 test = new Vector3(spawnPointFloor1[rand].transform.position.x, spawnPointFloor1[rand].transform.position.y, spawnPointFloor1[rand].transform.position.z);
                Instantiate(enemy, test, Quaternion.identity);
                currentEnemy++;
            }
        }
    }

    public void LeftUseMp(bool trueFalse)
    {
        if (trueFalse)
        {
            leftUsingMp = true;
        }
        else if (!trueFalse)
        {
            leftUsingMp = false;
        }
    }
    
    public void RightUseMp(bool trueFalse)
    {
        if (trueFalse)
        {
            rightUsingMp = true;
        }
        else if (!trueFalse)
        {
            rightUsingMp = false;
        }
    }

    public void Damage()
    {
        playerHp -= 20;
        damagePanel.GetComponent<Animator>().SetTrigger("Damage");
    }
    
    private void CheckCurrentWaveRemainEnemy()
    {
        /*if ()
        {
            remainEnemy = 1;
        }*/
    }
}
