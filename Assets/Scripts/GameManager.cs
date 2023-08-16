using System;
using MenuUI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score;
    public static GameManager Instance;
    [SerializeField] private AudioSource eventAudio;
    [SerializeField] private AudioClip gameOverClip;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        UIManager.Instance.SetTextScore(_score);
    }


    public void UpdateScore(int score)
    {
        if (score > _score)
        {
            _score = score;
            UIManager.Instance.SetTextScore(_score);
            CheckHighScore();
        }
    }

    private void CheckHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }
        UIManager.Instance.SetHighScoreText();
    }
    
    public void GameOver()
    {
        eventAudio.clip = gameOverClip;
        eventAudio.Play();
        UIManager.Instance.EnableGameOverPanel();
    }
}