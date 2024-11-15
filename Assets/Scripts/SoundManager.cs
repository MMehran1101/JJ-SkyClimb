using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource musicSource;
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

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
    
    public void MuteSound(bool isMute)
    {
        var soundAudio = gameObject.GetComponent<AudioSource>();
        soundAudio.mute = isMute;
    }
    public void PlayMusic(AudioClip clip, bool isLoop)
    {
        musicSource.PlayOneShot(clip);
        if (isLoop) musicSource.loop = true;
        else musicSource.loop = false;
    }
    public void MuteMusic(bool isMute)
    {
        var soundAudio = gameObject.GetComponent<AudioSource>();
        soundAudio.mute = isMute;
    }

}