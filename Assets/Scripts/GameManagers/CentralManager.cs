using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    private PowerupManager powerupManager;
    public static CentralManager centralManagerInstance;

    void Awake()
    {
        centralManagerInstance = this;
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerupManager = gameManagerObject.GetComponent<PowerupManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void restartGame()
    {
        SceneManager.LoadScene("SampleScene");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerupManager = gameManagerObject.GetComponent<PowerupManager>();
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void registerEnemy(int enemyID, SpawnManager manager)
    {
        gameManager.registerEnemy(enemyID, manager);
    }

    public void deadEnemy(int enemyID)
    {
        Debug.Log("Enemy Control Logged");
        gameManager.deadEnemy(enemyID);
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    public void coinStolen()
    {
        gameManager.coinStolen();
    }

    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerupManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        powerupManager.addPowerup(t, i, c);
    }

    public void setGodMode(bool state)
    {
        gameManager.setGodMode(state);
    }
}
