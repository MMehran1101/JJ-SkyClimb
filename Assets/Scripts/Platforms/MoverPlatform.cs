using DG.Tweening;
using Managers;
using UnityEngine;
using Utilities;

namespace Platforms
{
    public class MoverPlatform : MonoBehaviour
    {
        private Vector2 screenSize;
        private float _currentVelocity;
        private float _jumpVelocity = 350;
        private Rigidbody2D _moverRb;
        [SerializeField] private AudioClip jumpAudio;

        [SerializeField] private AnimationCurve platformEase;
        private Sequence moveSequence;
        private bool moveRight = true;
        private float moveDuration = 3;
        private float moveDistance;


        private void Start()
        {
            screenSize = ScreenUtils.GetCameraSize();
            moveDistance = screenSize.x + (ScreenUtils.GetObjectOffset(gameObject) / 2);
            AnimateMoving();
        }

        private void LateUpdate()
        {
            if (gameObject.transform.position.y < screenSize.y)
                Destroy(gameObject, 1);
        }

        private void AnimateMoving()
        {
            float targetPositionX;
            if (moveRight)
                targetPositionX = moveDistance;
            else
                targetPositionX = -moveDistance;

            transform.DOMoveX(targetPositionX, moveDuration)
                .SetEase(platformEase)
                .OnComplete(() =>
                {
                    moveRight = !moveRight;
                    AnimateMoving();
                });
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _currentVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            if (col.gameObject.CompareTag("Player"))
            {
                if (_currentVelocity <= 0)
                {
                    SoundManager.Instance.SetSoundClip(jumpAudio);

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