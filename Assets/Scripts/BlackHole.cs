using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
