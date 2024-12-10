using System;
using Managers;
using UnityEngine;
using Utilities;

namespace PlayerFL
{
    public class Player : MonoBehaviour
    {
        private float moveSpeed = 5f;
        float maxSpeed = 25f;
        float maxDistance = 10f;

        // gyro properties
        private float moveSpeedInGyro = 1000;
        private float smoothTime = 1.5f;

        private SpriteRenderer _spriteRenderer;

        private Vector2 currentVelocity = Vector2.zero;
        private Vector2 bounds;
        private Vector3 lastTouchPos;
        private Vector3 direction;
        private Camera m_Camera;


        private void Start()
        {
            Input.gyro.enabled = true;

            
            m_Camera = Camera.main;
            bounds = ScreenUtils.GetWorldScreenSize();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            PlayerControllerWithGyroscope();
            CheckXBounds();
        }

        private void PlayerControllerWithGyroscope()
        {
            
            if (Input.gyro.enabled)
            {
                float gyroInput = Input.acceleration.x;

                var position = transform.position;

                Vector2 targetPosition = new Vector2(gyroInput * moveSpeedInGyro * Time.deltaTime, position.y);

                FlipSprite(_spriteRenderer, position.x, targetPosition.x, .2f);

                position = Vector2.SmoothDamp(position, targetPosition, ref currentVelocity, smoothTime);
                transform.position = position;
            }
        }

        /*private void PlayerControllerWithTuch()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = m_Camera!.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                touchPos.z = 0;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        lastTouchPos.x = touchPos.x;
                        break;

                    case TouchPhase.Moved:
                        direction.x = (touchPos.x - lastTouchPos.x);

                        float distance = Vector3.Distance(touchPos, lastTouchPos);
                        float touchSpeed = distance / Time.deltaTime;

                        Mathf.Clamp(distance, 0, maxDistance);
                        touchSpeed = Mathf.Clamp(touchSpeed, 0, maxSpeed);

                        transform.position += direction * (touchSpeed * moveSpeed * Time.deltaTime);

                        FlipSprite(_spriteRenderer, lastTouchPos.x, touchPos.x, 0);

                        lastTouchPos.x = touchPos.x;
                        break;

                    case TouchPhase.Ended:
                        direction = Vector3.zero;
                        break;
                }
            }
        }
*/

        private void FlipSprite(SpriteRenderer spriteR, float position, float nextPostition, float offset)
        {
            if (position + offset < nextPostition)
                spriteR.flipX = false;
            else if (position - offset > nextPostition)
                spriteR.flipX = true;
        }

        private void CheckXBounds()
        {
            // calculate phone screen size
            var b = bounds.x / 2;

            var position = transform.position;
            if (position.x > b)
            {
                position = new Vector2(-b, position.y);
            }
            else if (position.x < -b)
            {
                position = new Vector2(b, position.y);
            }

            transform.position = position;
        }
        
    }
}