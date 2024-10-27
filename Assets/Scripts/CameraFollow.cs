using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private const float smoothTime = 0.2f;
    private Vector2 _currentVelocity;


    private void LateUpdate()
    {
        if (transform.position.y < playerTransform.position.y)
        {
            Vector2 newPos = new Vector2(transform.position.x, playerTransform.position.y);
            transform.position = Vector2.SmoothDamp(transform.position
                , newPos, ref _currentVelocity,
                smoothTime * Time.deltaTime);
        }
    }
}