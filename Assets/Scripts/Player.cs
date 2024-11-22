using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;
    private SpriteRenderer _spriteRenderer;

    private Vector3 lastTouchPos;
    private Vector3 direction;
    private Vector2 bounds;

    private void Start()
    {
        bounds = ScreenUtils.GetWorldScreenSize();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        PlayerControllerWithTuch();
        CheckXBounds();
    }

    private void LateUpdate()
    {
        CalculateScore();
    }
    
    private void PlayerControllerWithTuch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y,0));
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

                    float maxDistance = 10f; 
                    distance = Mathf.Clamp(distance, 0, maxDistance);

                    float maxSpeed = 10f; 
                    touchSpeed = Mathf.Clamp(touchSpeed, 0, maxSpeed);

                    transform.position += direction * (touchSpeed * moveSpeed * Time.deltaTime);
                    
                    // look player right or left sprite
                    if (touchPos.x > lastTouchPos.x)
                    {
                        _spriteRenderer.flipX = false;
                    }
                    else if (touchPos.x < lastTouchPos.x)
                    {
                        _spriteRenderer.flipX = true;
                    }

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