using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;
    float maxSpeed = 25f;
    float maxDistance = 10f;

    
    [SerializeField] private float moveSpeedInGyro = 1000;
    [SerializeField] private float smoothTime = 1.5f;

    
    private Action controlMethode;
    
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 bounds;
    private Vector3 lastTouchPos;
    private Vector3 direction;

    private void Start()
    {
        bounds = ScreenUtils.GetWorldScreenSize();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        int gyro = DataPersistence.LoadInt(DataPersistence.isGyroKey, 0);
        if (gyro == 0)
        {
            controlMethode = PlayerControllerWithTuch;
        }
        else if (gyro == 1)
        {
            controlMethode = PlayerControllerWithGyroscope;
        }
    }

    private void FixedUpdate()
    {
        controlMethode?.Invoke();

        CheckXBounds();
    }

    private void LateUpdate()
    {
        CalculateScore();
    }

    private void PlayerControllerWithGyroscope()
    {
        if (Input.gyro.enabled)
        {
            float gyroInput = Input.acceleration.x;

            var position = transform.position;

            Vector2 targetPosition = new Vector2(gyroInput * moveSpeedInGyro * Time.deltaTime, position.y);

            if (position.x < targetPosition.x)
                _spriteRenderer.flipX = false;
            else if (position.x > targetPosition.x)
                _spriteRenderer.flipX = true;

            position = Vector2.SmoothDamp(position, targetPosition, ref currentVelocity, smoothTime);
            transform.position = position;
        }
    }

    private void PlayerControllerWithTuch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
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

                    distance = Mathf.Clamp(distance, 0, maxDistance);

                    touchSpeed = Mathf.Clamp(touchSpeed, 0, maxSpeed);

                    transform.position += direction * (touchSpeed * moveSpeed * Time.deltaTime);

                    // look player right or left sprite
                    if (touchPos.x > lastTouchPos.x)
                        _spriteRenderer.flipX = false;
                    else if (touchPos.x < lastTouchPos.x)
                        _spriteRenderer.flipX = true;

                    lastTouchPos.x = touchPos.x;
                    break;

                case TouchPhase.Ended:
                    direction = Vector3.zero;
                    break;
            }
        }
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

    private void CalculateScore()
    {
        var score = transform.position.y * 10;
        GameManager.Instance.UpdateScore((int)score);
    }
}