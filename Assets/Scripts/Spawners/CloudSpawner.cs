using Managers;
using UnityEngine;
using Utilities;

namespace Spawners
{
    public class CloudSpawner : MonoBehaviour
    {
        private Vector3 spawnPos;
        private float minSpawnRange = 1;
        private float maxSpawnRange = 5;
        [SerializeField] private GameObject[] cloudsPrefab;


        private void Start()
        {
            spawnPos = new Vector3();
        }

        private void SpawnCloud()
        {
            var bounds = ScreenUtils.GetWorldScreenSize();
            var randomCloud = Random.Range(0, cloudsPrefab.Length);

            spawnPos.y += Random.Range(minSpawnRange, maxSpawnRange);
            spawnPos.x = Random.Range((-bounds.x / 2), (bounds.x / 2));

            if (SpawnManager.Instance.CheckObjectExist(spawnPos, .5f, LayerMask.GetMask("Enemy")))
            {
                Instantiate(cloudsPrefab[randomCloud], spawnPos, Quaternion.identity, gameObject.transform); 
            }
        
        }

        private void Update()
        {
            if (gameObject.transform.childCount <= 10)
            {
                Invoke(nameof(SpawnCloud),.1f);
            }
        }
    }
}