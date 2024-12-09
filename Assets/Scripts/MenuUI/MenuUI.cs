using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

namespace MenuUI
{
    public class MenuUI : MonoBehaviour
    {
        private Sequence jumpSequence;
        private Image soundSp;
        
        [Header("Settings Panel")]
        [SerializeField] private GameObject settingsPanel;
        
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI coinText;


        [Header("Sound")] [SerializeField] private AudioClip jumpClip;
        [SerializeField] private GameObject soundSprite;
        [SerializeField] private Sprite unmuteSound;
        [SerializeField] private Sprite muteSound;

        [Header("Elements")] [SerializeField] private GameObject leaderBoardPanel;
        [SerializeField] private RectTransform player;
        [SerializeField] private float jumpDuration;
        [SerializeField] private float jumpHeight;
        [SerializeField] private AnimationCurve playerEase;


        private void Start()
        {
            soundSp = soundSprite.GetComponent<Image>();
            DoodleJumping();
            CheckSound();
            SetTexts();
        }

        private void SetTexts()
        {
            coinText.text = DataPersistence.LoadInt(DataPersistence.coinKey, 0).ToString();
            highScoreText.text = DataPersistence.LoadInt(DataPersistence.highScoreKey, 0).ToString();
        }
        private void DoodleJumping()
        {
            if (player == null)
            {
                Debug.LogError("RectTransform is not assigned!");
                return;
            }

            jumpSequence = DOTween.Sequence();
            jumpSequence.Append(player.DOAnchorPosY(player.anchoredPosition.y + jumpHeight, jumpDuration)
                .SetEase(playerEase));

            jumpSequence.SetLoops(-1, LoopType.Yoyo)
                .OnStepComplete(() =>
                {
                    if (jumpSequence.CompletedLoops() % 2 == 0)
                    {
                        SoundManager.Instance.PlaySound(jumpClip);
                    }
                });
        }

        private void OnDisable()
        {
            if (jumpSequence != null)
            {
                jumpSequence.Kill();
            }
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void GyroBtn()
        {
            GameManager.Instance.SetGyro(true);
        }

        public void TouchMovementBtn()
        {
            GameManager.Instance.SetGyro(false);
        }
        
        public void OpenShop()
        {
            //Todo: Shop Section 
        }

        public void OpenLeaderBoardPanel()
        {
            //Todo: LeaderBoard Panel
        }

        private void CheckSound()
        {
            if (DataPersistence.LoadInt(DataPersistence.soundKey, 1) == 0)
            {
                soundSp.sprite = muteSound;
                SoundManager.Instance.MuteSound(true);
            }
            else
            {
                soundSp.sprite = unmuteSound;
                SoundManager.Instance.MuteSound(false);
            }
        }

        public void ChangeSound()
        {
            if (DataPersistence.LoadInt(DataPersistence.soundKey, 1) == 0)
            {
                soundSp.sprite = unmuteSound;
                DataPersistence.SaveInt(DataPersistence.soundKey, 1);
                SoundManager.Instance.MuteSound(false);
            }
            else
            {
                soundSp.sprite = muteSound;
                DataPersistence.SaveInt(DataPersistence.soundKey, 0);
                SoundManager.Instance.MuteSound(true);
            }
        }

        public void OpenSettings()
        {
            settingsPanel.SetActive(true);
        }
        public void CloseSettings()
        {
            settingsPanel.SetActive(false);
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