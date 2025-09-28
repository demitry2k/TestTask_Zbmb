using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelStats : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TMP_Text scoreCounterText;
    [SerializeField] private ResultsPanel _resultsPanel;
    public UnityEvent onGameOver;
    private void Awake()
    {
        UpdateScoreText();
    }
    public void AddPoints(int addedPoints)
    {
        score += addedPoints;
        UpdateScoreText();
    }
    public void RemovePoints(int removedPoints)
    {
        score -= removedPoints;
        if (score < 0)
        {
            score = 0;
        }
        UpdateScoreText();
    }
    public void BumpScore()
    {
        AddPoints(50);
    }
    private void UpdateScoreText()
    {
        scoreCounterText.text = score.ToString();
    }
    public void GameOver()
    {
        onGameOver.Invoke();
        _resultsPanel.Initialize(score);
    }
}
