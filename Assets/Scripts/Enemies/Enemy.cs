using System;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        private Transform cameraTransform;
        [SerializeField] private AudioSource audioSource;
        private float maxDistance = 10;

        private void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            if (Time.timeScale == 0) audioSource.mute = true; // if game has been stop
            float distance = Vector2.Distance(cameraTransform.position, transform.position);

            float volume = Mathf.Clamp01(1 - (distance / maxDistance));
            audioSource.volume = volume;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.GameOver();
                audioSource.mute = true;
            }
        }
    }
}