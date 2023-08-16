using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

namespace MenuUI
{
    public class MenuUIManager : MonoBehaviour
    {
        private bool _isSoundMute;
        [Header("Sound")]
        [SerializeField] private Image soundSprite; 
        [SerializeField] private Sprite unmuteSound; 
        [SerializeField] private Sprite muteSound; 
        
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void MuteSound()
        {
            if (_isSoundMute)
            {
                soundSprite.sprite = unmuteSound;
                _isSoundMute = false;
            }
            else
            {
                soundSprite.sprite = muteSound;
                _isSoundMute = true;
            }
        }
        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
            
        }
    }
}
