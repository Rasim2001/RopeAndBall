using Graf.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreManager
{
    private EventManager eventManager;
    private TextMeshProUGUI scoreText;

    public float score = 0;
    public void Init(EventManager eventManager, TextMeshProUGUI scoreText)
    {
        this.eventManager = eventManager;
        this.scoreText = scoreText;

        InitAction();
    }
    private void InitAction()
    {
        eventManager.OnScoreChangeEvent.AddListener(ChangeScoreText);
    }

    private void ChangeScoreText(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

}
