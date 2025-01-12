using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioSource effectSource;
        [SerializeField] private AudioSource musicSource;
        [Header("Audio Clips")]
        public AudioClip menuAudioClip;
        public AudioClip gameAudioClip;
        public AudioClip tapUIAudioClip;

        private bool isSoundMute;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetSoundClip(AudioClip clip)
        {
            effectSource.PlayOneShot(clip);

        }
        public void ToggleSound(bool isMute)
        {
            effectSource.mute = isMute;
        }
        
        public void SetMusicClip(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }

        public void PauseMusic()
        {
            musicSource.Pause();
        }

        public void ResumeMusic()
        {
            musicSource.Play();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void ToggleMusic(bool isMute)
        {
            musicSource.mute = isMute;
        }
    }
}