using Managers;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}