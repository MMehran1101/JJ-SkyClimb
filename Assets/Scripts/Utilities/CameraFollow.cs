using Managers;
using UnityEngine;

namespace Utilities
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform playerTransform;
        private const float smoothTime = 0.2f;
        private Vector2 _currentVelocity;

        private void Start()
        {
            // Mobile Screen Never Shut Down
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            GameObject player = GameManager.Instance.player.gameObject;
            playerTransform = player.GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            if (playerTransform != null && transform.position.y < playerTransform.position.y)
            {
                Vector2 newPos = new Vector2(transform.position.x, playerTransform.position.y);
                transform.position = Vector2.SmoothDamp(transform.position
                    , newPos, ref _currentVelocity,
                    smoothTime * Time.deltaTime);
            }
        }
    }
}
