using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameOver;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI endCoinText;
    private bool isGameOver = false;
    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in startMenu.transform)
        {
            if (eachChild.name != "Score" && eachChild.name != "Powerups")
            {
                Debug.Log("Child Found. Name: " + eachChild.name);
                // Disable Each Child
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void GameOver(int score, int coins)
    {
        if (!isGameOver)
        {
            StartCoroutine(waitAWhile(score, coins));
            
        }
    }

    IEnumerator waitAWhile(int score, int coins)
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Game Over Triggered");
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        endScoreText.text = "Score\n"+score;
        endCoinText.text = "Coins\n"+coins;
        isGameOver = true;
    }

    public void RestartGame()
    {
        Debug.Log("Game Restarted");
        CentralManager.centralManagerInstance.restartGame();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

    }
}
