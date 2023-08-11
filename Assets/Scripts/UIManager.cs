using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI scoreText;
   public static UIManager Instance;

   private void Awake()
   {
      Instance = this;
   }

   public void SetTextScore(int score)
   {
      scoreText.text = score.ToString();
   }
}
