using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlatform : MonoBehaviour
{
    private float _currentVelocity;
    private float _jumpVelocity = 350;
    private Rigidbody2D _moverRb;

    private void Start()
    {
        _moverRb = gameObject.GetComponent<Rigidbody2D>();
        _moverRb.AddForce(Vector2.right * 2000 * Time.deltaTime, ForceMode2D.Force);
    }

    private void Update()
    {
        switch (transform.position.x)
        {
            case < -2f:
                _moverRb.AddForce(Vector2.right * 200 * Time.deltaTime, ForceMode2D.Force);
                break;
            case > 2f:
                _moverRb.AddForce(Vector2.left * 200 * Time.deltaTime, ForceMode2D.Force);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _currentVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        if (col.gameObject.CompareTag("Player"))
        {
            if (_currentVelocity <= 0)
            {
                var playerRb = col.collider.GetComponent<Rigidbody2D>();
                if (playerRb == null) return;
                var playerVelocity = playerRb.velocity;
                playerVelocity.y = _jumpVelocity * Time.deltaTime;
                playerRb.velocity = playerVelocity;
            }
        }
    }
}