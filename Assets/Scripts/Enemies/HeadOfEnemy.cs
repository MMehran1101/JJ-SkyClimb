using UnityEngine;

namespace Enemies
{
    public class HeadOfEnemy : MonoBehaviour
    {
        private float _currentVelocity;
        private float _jumpVelocity = 350;
        private int _health = 1;
        [SerializeField] private Sprite skin;

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
                    if (gameObject.CompareTag("DoubleHealth"))
                    {
                        if (_health == 1)
                        {
                            _health = 0;
                            gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = skin;
                        }
                        else
                            Destroy(transform.parent.gameObject);
                    }
                    else
                        Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}