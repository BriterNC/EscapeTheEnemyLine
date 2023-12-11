using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject restartButton, resetButton;

    public GameObject bossHpPanel;

    public GameObject[] door;
    public GameObject[] bossDoor;
    public GameObject enemyBoss;
    public bool isBossPhrase;
    public GameObject[] tempEnemySet;
    
    public GameObject[] led;
    
    public GameObject monitorPanel;
    public Material ledRed, ledGreen;

    public GameObject[] ledShipEntrance;
    public Material ledRedShipEntrance, ledGreenShipEntrance;

    public GameObject missionCompleteSound;
    
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
        //StartWave();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetWave()
    {
        if (currentWave == 1)
        {
            foreach (GameObject enemy in tempEnemySet)
            {
                enemy.GetComponent<EnemyController>().Respawn();
            }
        }
        else if (currentWave == 2)
        {
            enemyBoss.GetComponent<EnemyController>().Respawn();
        }
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
        currentWaveTextMesh.text = "Wave: 1 / 2";
        
        waveStarted = true;
        startButton.SetActive(false);
        alertSound.SetActive(true);
        
        restartButton.SetActive(true);
        resetButton.SetActive(true);
    }

    /*public void FindEnemyLimit()
    {
        if (currentWave == assignedEnemyInWave[currentWave - 1])
        {
            remainEnemy = assignedEnemyInWave[currentWave];
        }
    }*/
    
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
            resetButton.SetActive(false);
            
            gameOver = true;
            
            // Continue
        }
        
        //CheckCurrentWaveRemainEnemy();

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
        
        //currentWaveTextMesh.text = $"Wave: {currentWave} / {totalWave}";
        
        if (isBossPhrase)
        {
            switch (enemyBoss.GetComponent<EnemyController>().bossHp)
            {
                case (5):
                    bossHpPanel.transform.GetChild(0).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(1).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(2).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(3).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(4).gameObject.SetActive(true);
                    break;
                case (4):
                    bossHpPanel.transform.GetChild(0).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(1).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(2).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(3).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(4).gameObject.SetActive(false);
                    break;
                case (3):
                    bossHpPanel.transform.GetChild(0).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(1).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(2).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(3).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(4).gameObject.SetActive(false);
                    break;
                case (2):
                    bossHpPanel.transform.GetChild(0).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(1).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(2).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(3).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(4).gameObject.SetActive(false);
                    break;
                case (1):
                    bossHpPanel.transform.GetChild(0).gameObject.SetActive(true);
                    bossHpPanel.transform.GetChild(1).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(2).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(3).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(4).gameObject.SetActive(false);
                    break;
                case (0):
                    bossHpPanel.transform.GetChild(0).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(1).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(2).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(3).gameObject.SetActive(false);
                    bossHpPanel.transform.GetChild(4).gameObject.SetActive(false);
                    break;
            }
            
            if (!enemyBoss.activeSelf)
            {
                foreach (GameObject ledTemp in led)
                {
                    ledTemp.GetComponent<MeshRenderer>().material = ledGreen;
                }
                foreach (GameObject ledTemp in ledShipEntrance)
                {
                    ledTemp.GetComponent<MeshRenderer>().material = ledGreenShipEntrance;
                }
                monitorPanel.GetComponent<Image>().color = Color.green;
                alertSound.SetActive(false);
                missionCompleteSound.SetActive(true);
                resetButton.SetActive(false);
            }
            return;
        }
        
        if (!isBossPhrase && tempEnemySet[0].activeSelf == false && tempEnemySet[1].activeSelf == false/* && tempEnemySet[2] == null && tempEnemySet[3] == null*/)
        {
            Debug.Log("All enemy died!");
            bossHpPanel.SetActive(true);
            isBossPhrase = true;
            BossPhrase();
        }
        
        /*if (spawnedEnemyFloor1 < enemyFloor1Limit)
        {
            SpawnEnemy(enemyGameObject, 1);
        }
        if (spawnedEnemyFloor2 < enemyFloor2Limit)
        {
            SpawnEnemy(enemyGameObject, 2);
        }*/
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

    private void BossPhrase()
    {
        currentWaveTextMesh.text = "Wave: 2 / 2";
        currentWave = 2;
        enemyBoss.SetActive(true);
        foreach (GameObject door in bossDoor)
        {
            door.GetComponent<Animator>().SetBool("character_nearby", true);
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
    
    /*private void CheckCurrentWaveRemainEnemy()
    {
        /*if ()
        {
            remainEnemy = 1;
        }#1#
    }*/
}
