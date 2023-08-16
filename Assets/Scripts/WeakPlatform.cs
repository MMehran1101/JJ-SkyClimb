using System;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{
    [SerializeField] private Sprite weakBrokenPlatform;
    private float _currentVelocity;
    [SerializeField] private AudioSource jumpAudio;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _currentVelocity = col.gameObject.GetComponent<Rigidbody2D>().velocity.y;
            if (_currentVelocity <= 0)
            {
                jumpAudio.Play();
                gameObject.GetComponent<SpriteRenderer>().sprite = weakBrokenPlatform;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }    }
}