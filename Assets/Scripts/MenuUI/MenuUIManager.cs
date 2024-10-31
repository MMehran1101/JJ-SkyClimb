using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

namespace MenuUI
{
    public class MenuUIManager : MonoBehaviour
    {
        private bool _isSoundMute;
        public static MenuUIManager Instance;
        [Header("Sound")]
        [SerializeField] private GameObject soundSprite; 
        [SerializeField] private Sprite unmuteSound; 
        [SerializeField] private Sprite muteSound;
        

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
        public void OpenShop()
        {
            SceneManager.LoadScene("Shop");
        }
        public void MuteSound()
        {
            var soundSp = soundSprite.GetComponent<Image>();
            if (_isSoundMute)
            {
                soundSp.sprite = unmuteSound;
                _isSoundMute = false;
                SoundManager.Instance.MuteSound(false);
            }
            else
            {
                soundSp.sprite = muteSound;
                _isSoundMute = true;
                SoundManager.Instance.MuteSound(true);
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
