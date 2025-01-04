using UnityEngine;
using Utilities;

namespace PowerUps
{
    public class SpringPowerUp : PowerUp
    {
        private Collider2D playerCol;
        private Rigidbody2D playerRb;
        private float _currentVelocity;
        
        private void Update()
        {
            if(gameObject.transform.position.y < ScreenUtils.GetCameraSize().y)
                Destroy(gameObject,1);
        }
        public override void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                playerCol = col.gameObject.GetComponent<Collider2D>();
                playerRb = col.gameObject.GetComponent<Rigidbody2D>();
                _currentVelocity = playerRb.velocity.y;
                Acivate(col.gameObject);
            }
        }
        public override void Acivate(GameObject player)
        {
            if (_currentVelocity <= 0)
            {
                playerCol.isTrigger = false;
                var playerVelocity = playerRb.velocity;
                playerVelocity.y = 700 * Time.deltaTime;
                playerRb.velocity = playerVelocity;
                
            }
        }
    }
}