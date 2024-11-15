using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _movement;
    private float _moveSpeed = 200;
    private Rigidbody2D _playerRb;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _playerRb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _movement = Input.GetAxis("Horizontal") * (_moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        PlayerController();
        CalculatePlayerVelocity();
        CheckXBounds();
    }

    private void LateUpdate()
    {
        CalculateScore();
    }

    private void CalculatePlayerVelocity()
    {
        var xVelocity = _playerRb.velocity;
        xVelocity.x = _movement;
        _playerRb.velocity = xVelocity;
    }

    private void PlayerController()
    {
        // Flip sprite when character going left of right
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _spriteRenderer.flipX = true;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _spriteRenderer.flipX = false;
    }

    private void CheckXBounds()
    {
        var position = transform.position;
        position = position.x switch
        {
            > 3 => new Vector2(-3.5f, position.y),
            < -3.5f => new Vector2(3, position.y),
            _ => position
        };
        transform.position = position;
    }

    private void CalculateScore()
    {
        var score = transform.position.y * 10;
        GameManager.Instance.UpdateScore((int) score);
    }
    
}