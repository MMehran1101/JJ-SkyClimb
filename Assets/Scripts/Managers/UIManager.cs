using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI scoreTextOnGameOver;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI coinText;
        
        [Header("Panels")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject pausePanel;
        public static UIManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            SetCoinText(GameManager.Instance.GetCoins());
            SetScoreText(GameManager.Instance.UpdateScore());
            SetHighScoreText(GameManager.Instance.CheckHighScore());
        }

        #region Buttons

        public void PauseGame()
        {
            pausePanel.SetActive(true);
            SoundManager.Instance.ToggleMusic(true);
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            pausePanel.SetActive(false);
            SoundManager.Instance.ToggleMusic(false);
            Time.timeScale = 1;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void RestartGame()
        {
            SoundManager.Instance.ToggleMusic(false);
            GameManager.Instance.RestartGame();
        }

        #endregion
        
        #region Texts Elements

        private void SetScoreText(int score)
        {
            scoreText.text = score.ToString();
            scoreTextOnGameOver.text = scoreText.text;
        }

        private void SetHighScoreText(int highScore)
        {
            highScoreText.text = highScore.ToString();
        }

        private void SetCoinText(int coins)
        {
            coinText.text = coins.ToString();
        }

        #endregion
        
        public void EnableGameOverPanel()
        {
            gameOverPanel.SetActive(true);
        }
    }
}