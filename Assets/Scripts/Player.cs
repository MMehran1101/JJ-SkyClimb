using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horizontalInput;
    private float _playerMoveSpeed = 400;
    private Rigidbody2D _playerRb;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _playerRb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal") * (Time.deltaTime * _playerMoveSpeed);
        // Flip sprite when character going left of right
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _spriteRenderer.flipX = true;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _spriteRenderer.flipX = false;
    }

    private void FixedUpdate()
    {
        var xVelocity = _playerRb.velocity;
        xVelocity.x = _horizontalInput;
        _playerRb.velocity = xVelocity;
    }
}