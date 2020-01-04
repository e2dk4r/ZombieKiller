using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    int score = 0;
    int highScore = 0;

    Text scoreText;
    Text highScoreText;
    Text newHighScoreText;
    void Awake()
    {
        newHighScoreText = GameObject.Find("NewHighScoreText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
    }
    void Start()
    {
        newHighScoreText.enabled = false;
        if (GameManager.instance.newHighScore)
            newHighScoreText.enabled = true;

        score = GameManager.instance.score;
        highScore = GameManager.instance.highScore;

        scoreText.text = $"SCORE: { score }";
        highScoreText.text = $"HIGH SCORE: { highScore }";
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
