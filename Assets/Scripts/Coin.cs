using System;
using DG.Tweening;
using Managers;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Sprite[] coinSprites;
    [SerializeField] private float frameDuration = 0.1f;
    [SerializeField] private AudioClip CoinCollectAudioClip;
    private Sequence coinSequence;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AnimateCoin();
    }

    private void AnimateCoin()
    {
        coinSequence = DOTween.Sequence();

        foreach (var sprite in coinSprites)
        {
            if (spriteRenderer)
            {
                coinSequence.AppendCallback(() => spriteRenderer.sprite = sprite)
                    .AppendInterval(frameDuration);
            }
        }

        coinSequence.SetLoops(-1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.SetCoin(2);
            SoundManager.Instance.SetSoundClip(CoinCollectAudioClip);
            coinSequence.Kill();
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        coinSequence.Kill();
    }
}