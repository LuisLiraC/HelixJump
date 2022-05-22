using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int highScore;
    public int currentScore;
    public int currentLevel = 0;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        UIManager.instance.UpdateHighScore(highScore);
    }

    void Update()
    {
        
    }

    public void NextLevel()
    {
        Debug.Log("Next level");
    }

    public void RestartLevel()
    {
        currentScore = 0;
        UIManager.instance.UpdateScore(currentScore);
        FindObjectOfType<BallController>().ResetBall();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UIManager.instance.UpdateScore(currentScore);

        if (currentScore > highScore)
        {
            highScore = currentScore;
            UIManager.instance.UpdateHighScore(highScore);
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }
}
