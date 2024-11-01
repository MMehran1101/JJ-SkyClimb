using System;
using MenuUI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score;
    private int coins;
    private BoxCollider2D _playerCollider;
    public static GameManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource eventAudio;
    [SerializeField] private AudioClip gameOverClip;
    
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SetCoin(int newCoin)
    {
        coins += newCoin;
        PlayerPrefs.SetInt("Coin",coins);
        Debug.Log(newCoin+" Coin recived. and inventori is " + coins);
        Debug.LogError("Total Coin: " + PlayerPrefs.GetInt("Coin",0));
    }
    public int GetCoin()
    {
        return coins;
    }
    void Start()
    {
        _score = 0;
        _playerCollider = _player.gameObject.GetComponent<BoxCollider2D>();
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
        BoxCollider2D.Destroy(_playerCollider);
        eventAudio.clip = gameOverClip;
        eventAudio.Play();
        UIManager.Instance.EnableGameOverPanel();
    }
}