using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text highScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        currentScoreText.text = $"Score: {score}";
    }

    public void UpdateHighScore(int score)
    {
        highScoreText.text = $"High Score: {score}";
    }
}
