using PlayerFL;
using UnityEngine;

namespace PowerUps
{
    public interface IPowerUp
    {
        void Acivate(GameObject player);
        void OnCollisionEnter2D(Collision2D col);

    }

    public abstract class PowerUp : MonoBehaviour, IPowerUp
    {
        public abstract void Acivate(GameObject player);
        public abstract void OnCollisionEnter2D(Collision2D col);
    }
}