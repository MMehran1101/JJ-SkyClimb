using System;
using PlayerFL;
using UnityEngine;

namespace PowerUps
{
    public class SpringPowerUp : PowerUp
    {
        public override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Acivate(col.gameObject);
            }
        }

        public override void Acivate(GameObject player)
        {
            //TODO: ***********************************
        }
    }
}
