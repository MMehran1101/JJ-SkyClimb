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

    public void SetCoin(int newCoin)
    {
        coins += newCoin;
        DataPersistence.SaveInt(DataPersistence.coinKey, coins);
    }

    void OnEnable()
    {
        // ثبت رویداد برای بارگذاری صحنه جدید
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // حذف رویداد
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("new scene : " + scene.name);
        if (scene.name == "Main Game")
        {
            player = Instantiate(playerPrefab, new Vector3(0, 2), quaternion.identity);
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

    void Start()
    {
        _playerCollider = playerPrefab.gameObject.GetComponent<BoxCollider2D>();
    }

    public void UpdateScore(int score)
    {
        if (score > _score)
        {
            _score = score;
            CheckHighScore();
        }
        UIManager.Instance.SetTextScore(_score);
    }

    private void CheckHighScore()
    {
        if (_score > DataPersistence.LoadInt(DataPersistence.highScoreKey, 0))
        {
            DataPersistence.SaveInt(DataPersistence.highScoreKey, _score);
            UIManager.Instance.SetHighScoreText(_score);
        }
    }

    public void GameOver()
    {
        BoxCollider2D.Destroy(_playerCollider);
        SoundManager.Instance.PlaySound(gameOverClip);
        UIManager.Instance.EnableGameOverPanel();
    }
}