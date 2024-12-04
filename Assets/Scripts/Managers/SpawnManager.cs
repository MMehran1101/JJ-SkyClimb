using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Platform Settings")] [SerializeField]
        private GameObject normalPlatform;

        [SerializeField] private GameObject moverPlatform;
        [SerializeField] private GameObject weakPlatform;

        [Header("Enemies")] [SerializeField] private GameObject[] enemies;

        [Header("Clouds")] [SerializeField] private GameObject[] clouds;
    
        private bool _isInstantiate = true;
        private int _platformsCount;
        private int _cloudsCount = 200;
        private Vector2 _spawnPos;
        private Vector2 bounds;
        private float platformOffset;

        private void Start()
        {
            platformOffset = ScreenUtils.GetObjectOffset(normalPlatform);

            InstantiateCloudes();

            _spawnPos = new Vector2(0, -2);
            bounds = ScreenUtils.GetWorldScreenSize();
            StartCoroutine(InstantiateRoutine());
        }

        private void Update()
        {
            if (_isInstantiate && _platformsCount <= 500)
                StartCoroutine(InstantiateRoutine());
        }

        private void InstantiateCloudes()
        {
            Vector2 cloudPos = new Vector2(0, -2);
            for (int i = 0; i < _cloudsCount; i++)
            {
                cloudPos.y += Random.Range(1.0f, 6.0f);
                cloudPos.x = Random.Range((-bounds.x / 2) + 1, (bounds.x / 2) - 1);
                Instantiate(clouds[Random.Range(0, 8)], cloudPos
                    , quaternion.identity);
            }
        }

        private IEnumerator InstantiateRoutine()
        {
            _isInstantiate = false;

            NormalPlatform();
        
            WeakandMoverPlatform();

            yield return new WaitForSeconds(1);

            EnemyInstantiate();

            _platformsCount += 10;
            _isInstantiate = true;
        }

        private void NormalPlatform()
        {
            int normalCount = Random.Range(5, 10);
            for (int i = 0; i < normalCount; i++)
            {
                _spawnPos.y += Random.Range(.5f, 1);
                _spawnPos.x = Random.Range((-bounds.x / 2) + platformOffset, (bounds.x / 2) - platformOffset);
                Instantiate(normalPlatform, _spawnPos, quaternion.identity);
            }
        }

        private void WeakandMoverPlatform()
        {
            _spawnPos.y += Random.Range(.5f, 1);
            _spawnPos.x =  Random.Range((-bounds.x / 2) + platformOffset, (bounds.x / 2) - platformOffset);
            if (Random.Range(0, 2) == 1)
            {
                Instantiate(weakPlatform, _spawnPos, quaternion.identity);
            }

            Instantiate(moverPlatform, _spawnPos, quaternion.identity);
        }

        private void EnemyInstantiate()
        {
            //_spawnPos.y += Random.Range(-1f, 0f);
            _spawnPos.y += Random.Range(.5f, 1);
            _spawnPos.x = Random.Range((-bounds.x / 2) + platformOffset, (bounds.x / 2) - platformOffset);
            Instantiate(normalPlatform, _spawnPos, quaternion.identity);
            if (_platformsCount >= 50)
            {
                _spawnPos.y += Random.Range(1f, 1.5f);
                _spawnPos.x = Random.Range((-bounds.x / 2) + 1, (bounds.x / 2) - 1);
                Instantiate(enemies[Random.Range(0, 2)], _spawnPos, quaternion.identity);
            }
        }
    }
}