using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace MenuUI
{
    public class ButtonSound : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(PlayTapSound);
        }

        void PlayTapSound()
        {
            SoundManager.Instance.SetSoundClip(SoundManager.Instance.tapUIAudioClip);
        }
    }
}
