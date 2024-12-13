using System;
using Managers;
using PowerUps;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Platforms
{
    public class NormalPlatform : MonoBehaviour
    {
        private float _currentVelocity;
        private float _jumpVelocity = 350;
        [SerializeField] private AudioClip jumpAudio;
        [SerializeField] private PowerUp[] PowerUpsPrefab;

        private void Update()
        {
            if(gameObject.transform.position.y < ScreenUtils.GetCameraSize().y)
                Destroy(gameObject,1);
        }
    
        private void OnCollisionEnter2D(Collision2D col)
        {
            _currentVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            if (col.gameObject.CompareTag("Player"))
            {
                if (_currentVelocity <= 0)
                {
                    SoundManager.Instance.PlaySound(jumpAudio);

                    var playerRb = col.collider.GetComponent<Rigidbody2D>();
                    if (playerRb == null) return;
                    var playerVelocity = playerRb.velocity;
                    playerVelocity.y = _jumpVelocity * Time.deltaTime;
                    playerRb.velocity = playerVelocity;
                }
            }
        }
    }
}