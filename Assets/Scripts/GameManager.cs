using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int wave = 8;
    public int enemyCount = 0;
    public int score = 0;
    public int highScore = 0;
    public float waveDelayTime = 4.0f;
    public float showMysteryBoxInfoTime = 2.0f;
    public bool gameOver = false;
    public bool gamePaused = false;
    public bool newHighScore = false;
    public GameObject pauseMenuPanel;
    
    Text countdownText;
    Text gameOverText;
    Text waveText;
    Text scoreText;
    public Text mysteryBoxInfoText;
    private float timer = 0f;
    private float textTimer = 0f;
    private bool countdownStarted = false;
    private bool showTextCountdownStarted = false;

    void Awake()
    {
        if (instance == null)
            instance = this;

        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        mysteryBoxInfoText = GameObject.Find("MysteryBoxInfoText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
    }

    void Start()
    {
        gameOverText.enabled = gameOver;
        mysteryBoxInfoText.enabled = false;
        pauseMenuPanel.SetActive(gamePaused);

        if (!PlayerPrefs.HasKey("highScore"))
            PlayerPrefs.SetInt("highScore", 0);
        highScore = PlayerPrefs.GetInt("highScore");
    }

    void Update()
    {
        if (gameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        Cursor.visible = gamePaused;

        waveText.text = $"Wave: { wave - 1 }";
        scoreText.text = $"Score: { score }";

        
        if (showTextCountdownStarted)
        {
            if (textTimer <= 0)
            {
                showTextCountdownStarted = false;
                mysteryBoxInfoText.enabled = showTextCountdownStarted;
            }
            textTimer -= Time.deltaTime;
        }

        if (enemyCount == 0 && !countdownStarted)
        {
            timer = waveDelayTime;
            countdownStarted = true;
            countdownText.enabled = countdownStarted;
        }

        if (countdownStarted)
        {
            if (timer <= 0)
            {
                countdownStarted = false;
                countdownText.enabled = countdownStarted;
            }
            timer -= Time.deltaTime;
            countdownText.text = $"Wave Starts in { (int)timer } seconds";
        }

        if (enemyCount == 0 && !countdownStarted)
            NextWave();
    }

    void NextWave()
    {
        wave++;
        BoardManager.instance.SetupScene(wave);
    }

    public void ShowMystery(string text)
    {
        mysteryBoxInfoText.text = text;

        showTextCountdownStarted = true;
        mysteryBoxInfoText.enabled = showTextCountdownStarted;
        textTimer = showMysteryBoxInfoTime;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.enabled = gameOver;

        gamePaused = true;
        Time.timeScale = 0f;
        Cursor.visible = gamePaused;

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            newHighScore = true;
        }

        SceneManager.LoadScene("GameOverScene");
    }

    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0f;
        Cursor.visible = gamePaused;

        pauseMenuPanel.SetActive(gamePaused);
    }

    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        Cursor.visible = gamePaused;

        pauseMenuPanel.SetActive(gamePaused);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
