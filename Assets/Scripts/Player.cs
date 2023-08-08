using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horizontalInput;
    private float _playerMoveSpeed = 250;
    private Rigidbody2D _playerRb;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _playerRb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PlayerController();
    }
    private void FixedUpdate()
    {
        CalculatePlayerVelocity();
    }

    private void CalculatePlayerVelocity()
    {
        var xVelocity = _playerRb.velocity;
        xVelocity.x = _horizontalInput;
        _playerRb.velocity = xVelocity;
    }

    private void PlayerController()
    {
        _horizontalInput = Input.GetAxis("Horizontal") * (Time.deltaTime * _playerMoveSpeed);
        
        // Flip sprite when character going left of right
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _spriteRenderer.flipX = true;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _spriteRenderer.flipX = false;

        CheckXBounds();
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

   
}