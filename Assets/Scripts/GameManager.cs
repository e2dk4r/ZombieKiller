using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int wave = 8;
    public int enemyCount = 0;
    public float waveDelayTime = 4.0f;
    public bool gameOver = false;

    Text countdownText;
    Text gameOverText;
    Text waveText;
    private float timer = 0f;
    private bool countdownStarted = false;

    void Awake()
    {
        if (instance == null)
            instance = this;

        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
    }

    void Start()
    {
        gameOverText.enabled = gameOver;
    }

    void Update()
    {
        if (gameOver)
            return;

        waveText.text = $"Wave: { wave - 1 }";

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

    public void GameOver()
    {
        gameOver = true;
        gameOverText.enabled = gameOver;
    }
}
