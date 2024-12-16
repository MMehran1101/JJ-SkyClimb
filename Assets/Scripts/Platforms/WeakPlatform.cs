using Managers;
using UnityEngine;
using Utilities;

namespace Platforms
{
    public class WeakPlatform : MonoBehaviour
    {
        private float _currentVelocity;
        [SerializeField] private AudioClip jumpAudio;
        private bool isVibrate;

        private void Start()
        {
            if (GameManager.Instance.ReturnVibrationStatus())
                isVibrate = true;
        }
        
        private void Update()
        {
            if(gameObject.transform.position.y < ScreenUtils.GetCameraSize().y)
                Destroy(gameObject,1);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _currentVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.y;
                if (_currentVelocity <= 0)
                {
                    SoundManager.Instance.SetSoundClip(jumpAudio);
                    if (isVibrate) Vibration.Vibrate(100);

                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
                }
            }    }
    }
}