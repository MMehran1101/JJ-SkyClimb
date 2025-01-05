using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        private int platformWavesCount;
        public static SpawnManager Instance;
        [SerializeField] private List<GameObject> platformWaves;
        [SerializeField] private List<GameObject> enemyWaves;
        public Vector3 platformSpawnPos;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            platformWavesCount = 0;
            SpawnPlatforms();
        }

        private void Update()
        {
            // Check if platform waves less then 5 instantiate them
            if (platformWavesCount < 5)
            {
                Invoke(nameof(SpawnPlatforms), 0);
            }
        }

        private void SpawnPlatforms()
        {
            switch (GameManager.Instance.GetScore())
            {
                case < 100:
                    Instantiate(platformWaves[0], platformSpawnPos, quaternion.identity, gameObject.transform);
                    platformWavesCount += 1;
                    break;
                case > 100 and < 500:
                    Instantiate(platformWaves[1], platformSpawnPos, quaternion.identity, gameObject.transform);
                    platformWavesCount += 1;
                    EnemyChanceToSpawn(enemyWaves[0]);
                    break;
                case > 500 and < 1000:
                    Instantiate(platformWaves[2], platformSpawnPos, quaternion.identity, gameObject.transform);
                    platformWavesCount += 1;
                    EnemyChanceToSpawn(enemyWaves[0]);
                    break;
                case > 1000:
                    Instantiate(platformWaves[3], platformSpawnPos, quaternion.identity, gameObject.transform);
                    platformWavesCount += 1;
                    EnemyChanceToSpawn(enemyWaves[0]);
                    break;
            }
        }

        private void EnemyChanceToSpawn(GameObject enemy)
        {
            var chance = Random.Range(1, 10);
            if (chance % 2 == 0)
            {
                Instantiate(enemy, platformSpawnPos, quaternion.identity, gameObject.transform);
            }
        }

        public void DecreasePlatformWavesCount(int cuont)
        {
            platformWavesCount -= cuont;
        }

        public bool CheckObjectExist(Vector2 postionToSpawn, float spawnRedius, LayerMask checkleyer)
        {
            return Physics2D.OverlapCircle(postionToSpawn, spawnRedius, checkleyer) == null;
        }
    }
}