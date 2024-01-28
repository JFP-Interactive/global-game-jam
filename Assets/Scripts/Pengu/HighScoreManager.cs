using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] int meterPerPoint = 10;
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private TMPro.TMP_Text highScoreText;
    [SerializeField] private Transform playerTransform;
    private int currentScore = 0;
    private int highScore;

    public static HighScoreManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        if (!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetInt("highScore", 0);
            LoadHighScore();
        }
        else
        {
            LoadHighScore();
        }
    }

    private void Start()
    {
        highScoreText.text = highScore.ToString();
    }

    private void FixedUpdate()
    {
        currentScore = Mathf.Max(currentScore, (int)playerTransform.position.z / meterPerPoint);
        scoreText.text = currentScore.ToString();
    }

    public void SetHighScore()
    {
        highScore = Mathf.Max(highScore, currentScore);
        SaveHighScore();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("highScore");
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
    }

    public void GivePoints(int points)
    {
        currentScore += points;
    }
}