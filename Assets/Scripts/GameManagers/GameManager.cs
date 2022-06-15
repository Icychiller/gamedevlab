using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SpawnedEnemy
{
    public int enemyID;
    public SpawnManager spawnManager;

    public SpawnedEnemy(int enemyID, SpawnManager spawnManager)
    {
        this.enemyID = enemyID;
        this.spawnManager = spawnManager;
    }
}

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public MenuController menuController;
    private int playerScore = 0;
    private int playerCoins = 0;
    private List<SpawnedEnemy> spawnList = new List<SpawnedEnemy>();
    public bool godMode = false;

    public void setGodMode(bool state)
    {
        godMode = state;
    }

    public void increaseScore()
    {
        playerScore += 1;
        updateScoreText();
    }
    public void increaseCoins()
    {
        
    }

    private void updateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: \n" + playerScore.ToString();
        }
        else
        {
            Debug.Log("Missing Reference to scoreText in GameManager.cs");
        }
    }
    public void registerEnemy(int enemyID, SpawnManager manager)
    {
        spawnList.Add(new SpawnedEnemy(enemyID,manager));
    }

    public void deadEnemy(int enemyID)
    {
        foreach (SpawnedEnemy item in spawnList)
        {
            Debug.Log(item.enemyID + ":"+enemyID);
            if (enemyID == item.enemyID && enemyID != -1)
            {
                item.spawnManager.enemyDead();
                spawnList.Remove(item);
                return;
            }
        }
    }

    public void damagePlayer()
    {
        if (!godMode)
        {
            OnPlayerDeath();
            if (menuController != null)
            {
                menuController.GameOver(playerScore, playerCoins);
            }
        }
    }

    public void coinStolen()
    {
        OnCoinCollect();
        playerScore += 10;
        playerCoins += 1;
        updateScoreText();
    }

    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;

    public static event gameEvent OnCoinCollect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
