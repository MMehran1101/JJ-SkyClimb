using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private GameObject gameOverPanel;
   public static UIManager Instance;

   private void Awake()
   {
      Instance = this;
   }

   public void SetTextScore(int score)
   {
      scoreText.text = score.ToString();
   }

   public void EnableGameOverPanel()
   {
      gameOverPanel.SetActive(true);
   }
}
