using Managers;
using UnityEngine;

namespace Platforms
{
    public class WeakPlatform : MonoBehaviour
    {
        private float _currentVelocity;
        [SerializeField] private AudioClip jumpAudio;

        private void Update()
        {
            if(gameObject.transform.position.y < GameManager.Instance.GetCameraSize().y)
                Destroy(gameObject,1);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _currentVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.y;
                if (_currentVelocity <= 0)
                {
                    SoundManager.Instance.PlaySound(jumpAudio);
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
                }
            }    }
    }
}