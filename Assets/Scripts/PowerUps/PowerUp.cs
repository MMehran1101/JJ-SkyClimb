using PlayerFL;
using UnityEngine;

namespace PowerUps
{
    public interface IPowerUp
    {
        void Acivate(GameObject player);
        void OnTriggerEnter2D(Collider2D col);

    }

    public abstract class PowerUp : MonoBehaviour, IPowerUp
    {
        public abstract void Acivate(GameObject player);
        public abstract void OnTriggerEnter2D(Collider2D col);

    }
}