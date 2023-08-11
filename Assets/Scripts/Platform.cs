using UnityEngine;

public class Platform : MonoBehaviour
{
    private float _jumpVelocity = 350;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.relativeVelocity.y <= 0)
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