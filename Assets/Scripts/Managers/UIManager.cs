using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        private bool musicState;
        [Header("Texts")] [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI scoreTextOnGameOver;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI coinText;

        [Header("Panels")] [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject pausePanel;
        public static UIManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            SetCoinText(GameManager.Instance.GetCoins());
        }

        private void OnEnable()
        {
            EventManager.onScoreChanged += SetScoreText;
            EventManager.onGameOver += SetHighScoreText;
        }

        private void OnDisable()
        {
            EventManager.onScoreChanged -= SetScoreText;
            EventManager.onGameOver -= SetHighScoreText;
        }

        #region Buttons

        public void PauseGame()
        {
            pausePanel.SetActive(true);
            SoundManager.Instance.PauseMusic();
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            pausePanel.SetActive(false);
            SoundManager.Instance.ResumeMusic();
            Time.timeScale = 1;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void RestartGame()
        {
            GameManager.Instance.RestartGame();
        }

        #endregion

        #region Texts Elements

        private void SetScoreText(int score)
        {
            scoreText.text = score.ToString();
            scoreTextOnGameOver.text = scoreText.text;
        }

        private void SetHighScoreText()
        {
            highScoreText.text = DataPersistence.LoadInt(DataPersistence.highScoreKey, 0).ToString();
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