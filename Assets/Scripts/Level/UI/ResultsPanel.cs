using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ResultsPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    public void Initialize(int score)
    {
        _scoreText.text = score.ToString();
        gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        LevelLoader.Instance.ReloadCurrentScene();
    }
    public void ExitToMenu()
    {
        LevelLoader.Instance.LoadMainMenu();
    }
}
