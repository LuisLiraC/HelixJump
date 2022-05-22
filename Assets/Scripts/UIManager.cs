using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text highScoreText;

    public Slider slider;

    public Text currentLevel;
    public Text nextLevel;

    public Transform topTransform;
    public Transform goalTransform;

    public Transform ball;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        ChangeSliderLevelProgress();
    }

    public void UpdateScore(int score)
    {
        currentScoreText.text = $"Score: {score}";
    }

    public void UpdateHighScore(int score)
    {
        highScoreText.text = $"High Score: {score}";
    }

    public void ChangeSliderLevelProgress()
    {
        currentLevel.text = $"{GameManager.instance.currentLevel + 1}";
        nextLevel.text = $"{GameManager.instance.currentLevel + 2}";

        float totalDistance = (topTransform.position.y - goalTransform.position.y);
        float distanceLeft = totalDistance - (ball.position.y - goalTransform.position.y);

        float sliderValue = distanceLeft / totalDistance;
        slider.value = Mathf.Lerp(slider.value, sliderValue, 3);
    }
}
