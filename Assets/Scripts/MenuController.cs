using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{

    public PlayerController playerController;
    public GameObject startMenu;
    public GameObject gameOver;
    public TextMeshProUGUI endScoreText;
    private bool isGameOver = false;
    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in startMenu.transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child Found. Name: " + eachChild.name);
                // Disable Each Child
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over Triggered");
        // Animation Here?
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Debug.Log("Game Restarted");
        SceneManager.LoadScene("SampleScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.deathState && !isGameOver)
        {
            GameOver();
            
            gameOver.SetActive(true);
            endScoreText.text = "Score\n"+playerController.score;
            isGameOver = true;
        }
    }
}
