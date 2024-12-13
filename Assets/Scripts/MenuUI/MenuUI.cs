using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace MenuUI
{
    public class MenuUI : MonoBehaviour
    {
        private Sequence jumpSequence;
        private Image soundSp;

        [Header("Panels")] [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject leaderBoardPanel;
        [SerializeField] private GameObject exitAppPanel;

        [Header("Texts")] [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI coinText;


        [Header("Sound")] [SerializeField] private AudioClip jumpClip;
        [SerializeField] private GameObject soundSprite;
        [SerializeField] private Sprite unmuteSound;
        [SerializeField] private Sprite muteSound;

        [Header("Elements")] [SerializeField] private RectTransform player;
        [SerializeField] private float jumpDuration;
        [SerializeField] private float jumpHeight;
        [SerializeField] private AnimationCurve playerEase;


        private void Start()
        {
            soundSp = soundSprite.GetComponent<Image>();
            PlayerJumping();
            CheckSound();
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
                        SoundManager.Instance.PlaySound(jumpClip);
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