using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _score;
    private int coins;
    
    private BoxCollider2D _playerCollider;
    public static GameManager Instance;
    [SerializeField] private GameObject playerPrefab;
    [HideInInspector] public GameObject player;
    [SerializeField] private AudioClip gameOverClip;
    private bool isGameOver;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        PlayerOnScreen();
    }

    public void SetGyro(bool t)
    {
        int data = t ? 1 : 0;
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = t;
            DataPersistence.SaveInt(DataPersistence.isGyroKey, data);
        }
        else
        {
            Input.gyro.enabled = false;
            //todo: if player device not support show a pop up and tell him.
            Debug.LogWarning("Gyroscope is not supported on this device.");
        }
    }

    public void SetCoin(int newCoin)
    {
        coins += newCoin;
        DataPersistence.SaveInt(DataPersistence.coinKey, coins);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene : " + scene.name);
        if (scene.name == "Main Game")
        {
            player = Instantiate(playerPrefab, new Vector3(0, 2), quaternion.identity);
            _playerCollider = player.GetComponent<BoxCollider2D>();
            isGameOver = false;
            _score = 0;
            coins = 0;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetCoin()
    {
        return coins;
    }

    public void UpdateScore(int score)
    {
        if (score > _score)
        {
            _score = score;
        }

        UIManager.Instance.SetTextScore(_score);
    }

    private void CheckHighScore()
    {
        var highscore = DataPersistence.LoadInt(DataPersistence.highScoreKey, 0);

        if (_score > highscore)
        {
            UIManager.Instance.SetHighScoreText(_score);
            DataPersistence.SaveInt(DataPersistence.highScoreKey, _score);
        }
        else
            UIManager.Instance.SetHighScoreText(highscore);
    }

    private void PlayerOnScreen()
    {
        if (player != null && !isGameOver)
        {
            if (player.transform.position.y < GetCameraSize().y)
            {
                GameOver();
            }
        }
    }

    public Vector3 GetCameraSize()
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    }
    
    public void GameOver()
    {
        isGameOver = true;
        _playerCollider.isTrigger = true;

        CheckHighScore();

        SoundManager.Instance.PlaySound(gameOverClip);
        UIManager.Instance.EnableGameOverPanel();

        Destroy(player.gameObject, 2);
    }
}