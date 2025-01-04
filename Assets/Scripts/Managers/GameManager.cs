using Utilities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private int _score;
        private int _coins;
        private bool isGameOver = false;

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

        private void Update()
        {
            PlayerOnScreen();
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
                SoundManager.Instance.SetMusicClip(SoundManager.Instance.gameAudioClip);
                isGameOver = false;
                _score = 0;
                _coins = 0;
            }
        }

        private void PlayerOnScreen()
        {
            if (player != null && !isGameOver)
            {
                if (player.transform.position.y < ScreenUtils.GetCameraSize().y)
                {
                    GameOver();
                }
            }
        }

        public bool ReturnVibrationStatus()
        {
            var vibrate = DataPersistence.LoadInt(DataPersistence.vibrationKey, 1);
            if (vibrate == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        #region Score and Coins

        public int UpdateScore()
        {
            var score = player.transform.position.y * 10;

            if (score > _score)
            {
                _score = (int)score;
            }

            return _score;
        }

        public int CheckHighScore()
        {
            var highscore = DataPersistence.LoadInt(DataPersistence.highScoreKey, 0);

            if (_score > highscore)
            {
                DataPersistence.SaveInt(DataPersistence.highScoreKey, _score);
                return _score;
            }
            else
                return highscore;
        }

        public int GetScore()
        {
            return _score;
        }

        public void SetCoin(int newCoin)
        {
            _coins += newCoin;
        }

        public int GetCoins()
        {
            return _coins;
        }

        #endregion


        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GameOver()
        {
            if (!isGameOver)
            {
                isGameOver = true;
                _playerCollider.isTrigger = true;
                if (ReturnVibrationStatus()) Vibration.Vibrate(500);

                // add cions collected
                int totalCoin = DataPersistence.LoadInt(DataPersistence.coinKey, 0);
                totalCoin += _coins;
                DataPersistence.SaveInt(DataPersistence.coinKey, totalCoin);

                SoundManager.Instance.SetMusicClip(gameOverClip);
                UIManager.Instance.EnableGameOverPanel();

                Destroy(player.gameObject, 2);
            }
        }
    }
}