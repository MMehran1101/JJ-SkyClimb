using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Texts")] [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreTextOnGameOver;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [Header("Panels")] [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
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

    public void SetTextScore(int score)
    {
        scoreText.text = score.ToString();
        scoreTextOnGameOver.text = scoreText.text;
    }

    public void SetHighScoreText(int highScore)
    {
        highScoreText.text = highScore.ToString();
    }

    public void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}