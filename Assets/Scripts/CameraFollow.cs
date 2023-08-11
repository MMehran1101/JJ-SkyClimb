using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private const float SmoothTime = 0.2f;
    private Vector2 _currentVelocity;


    private void LateUpdate()
    {
        if (transform.position.y < playerTransform.transform.position.y)
        {
            var position = transform.position;
            var newPos = new Vector2(position.x, playerTransform.position.y);
            position = Vector2.SmoothDamp(position,
                newPos, ref _currentVelocity, SmoothTime); // smooth moving cam
            transform.position = position;
        }
    }

}