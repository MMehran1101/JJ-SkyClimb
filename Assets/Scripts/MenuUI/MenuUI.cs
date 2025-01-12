using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;
using Image = UnityEngine.UI.Image;

namespace MenuUI
{
    public class MenuUI : MonoBehaviour
    {
        private Sequence jumpSequence;
        private Image soundSp;
        private Image musicSp;
        private Image vibrationSp;

        [Header("Texts")] [SerializeField] 
        private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI coinText;
        [Header("Player On Menu Elements")] 
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private RectTransform player;
        [SerializeField] private float jumpDuration;
        [SerializeField] private float jumpHeight;
        [SerializeField] private AnimationCurve playerEase;
        [Header("Panels")] [SerializeField]
        private GameObject settingsPanel;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject leaderBoardPanel;
        [SerializeField] private GameObject exitAppPanel;
        [Header("Settings Elements")] 
        [SerializeField] private GameObject soundSprite;
        [SerializeField] private GameObject musicSprite;
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        [SerializeField] private Slider sensetiveSlider;
        [SerializeField] private GameObject vibrationSprite;

        private void Start()
        {
            SoundManager.Instance.SetMusicClip(SoundManager.Instance.menuAudioClip);
            PlayerJumping();
            LoadSettingsData();
            SetTexts();
        }

        private void OnDisable()
        {
            if (jumpSequence != null)
            {
                jumpSequence.Kill();
            }
        }
        
        private void SetTexts()
        {
            coinText.text = DataPersistence.LoadInt(DataPersistence.coinKey, 0).ToString();
            highScoreText.text = DataPersistence.LoadInt(DataPersistence.highScoreKey, 0).ToString();
        }

        private void PlayerJumping()
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
                        SoundManager.Instance.SetSoundClip(jumpClip);
                    }
                });
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void OpenShop()
        {
            shopPanel.SetActive(true);
        }

        public void CloseShop()
        {
            shopPanel.SetActive(false);
        }

        public void OpenLeaderBoard()
        {
            leaderBoardPanel.SetActive(true);
        }

        public void CloseLeaderBoard()
        {
            leaderBoardPanel.SetActive(false);
        }

        public void OpenSettings()
        {
            settingsPanel.SetActive(true);

        }

        public void CloseSettings()
        {
            settingsPanel.SetActive(false);
        }

        public void IncreaseSensetive()
        {
            sensetiveSlider.value += 5;
            DataPersistence.SaveInt(DataPersistence.gyroSensetiveKey,(int)sensetiveSlider.value);
        }
        public void DecreaseSensetive()
        {
            sensetiveSlider.value -= 5;
            DataPersistence.SaveInt(DataPersistence.gyroSensetiveKey,(int)sensetiveSlider.value);
        }
        private void LoadSettingsData()
        {
            soundSp = soundSprite.GetComponent<Image>();
            musicSp = musicSprite.GetComponent<Image>();
            vibrationSp = vibrationSprite.GetComponent<Image>();
            sensetiveSlider.value = DataPersistence.LoadInt(DataPersistence.gyroSensetiveKey, 10);
            
            // Sound Data
            if (DataPersistence.LoadInt(DataPersistence.soundKey, 1) == 0)
            {
                soundSp.sprite = offSprite;
                SoundManager.Instance.ToggleSound(true);
            }
            else
            {
                soundSp.sprite = onSprite;
                SoundManager.Instance.ToggleSound(false);
            }
            
            // Music Data
            if (DataPersistence.LoadInt(DataPersistence.musicKey, 1) == 0)
            {
                musicSp.sprite = offSprite;
                SoundManager.Instance.ToggleMusic(true);
            }
            else
            {
                musicSp.sprite = onSprite;
                SoundManager.Instance.ToggleMusic(false);
            }

            // Vibration Data
            if (DataPersistence.LoadInt(DataPersistence.vibrationKey, 1) == 0)
            {
                vibrationSp.sprite = offSprite;
            }
            else
            {
                vibrationSp.sprite = onSprite;
            }
        }

        public void OnClickSound()
        {
            if (DataPersistence.LoadInt(DataPersistence.soundKey, 1) == 0)
            {
                soundSp.sprite = onSprite;
                DataPersistence.SaveInt(DataPersistence.soundKey, 1);
                SoundManager.Instance.ToggleSound(false);
            }
            else
            {
                soundSp.sprite = offSprite;
                DataPersistence.SaveInt(DataPersistence.soundKey, 0);
                SoundManager.Instance.ToggleSound(true);
            }
        }
        
        public void OnClickMusic()
        {
            if (DataPersistence.LoadInt(DataPersistence.musicKey, 1) == 0)
            {
                musicSp.sprite = onSprite;
                DataPersistence.SaveInt(DataPersistence.musicKey, 1);
                SoundManager.Instance.ToggleMusic(false);
            }
            else
            {
                musicSp.sprite = offSprite;
                DataPersistence.SaveInt(DataPersistence.musicKey, 0);
                SoundManager.Instance.ToggleMusic(true);
            }
        }

        public void OnClickVibration()
        {
            if (DataPersistence.LoadInt(DataPersistence.vibrationKey, 1) == 0)
            {
                vibrationSp.sprite = onSprite;
                DataPersistence.SaveInt(DataPersistence.vibrationKey, 1);
            }
            else
            {
                vibrationSp.sprite = offSprite;
                DataPersistence.SaveInt(DataPersistence.vibrationKey, 0);
            }
        }
        public void ExitAppPanel()
        {
            exitAppPanel.SetActive(true);
        }

        public void ExitAppCanceled()
        {
            exitAppPanel.SetActive(false);
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