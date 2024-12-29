using Managers;
using UnityEngine;
using Utilities;

public class EnemySpawner : MonoBehaviour
{
    private float enemyOffset;


    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float spawnRedius;
    [SerializeField] private float minSpawnRange;
    [SerializeField] private float maxSpawnRange;

    private void Start()
    {
        var randomPrefab = Random.Range(0, enemyPrefab.Length);
        enemyOffset = ScreenUtils.GetObjectOffset(enemyPrefab[randomPrefab]);
        var bounds = ScreenUtils.GetWorldScreenSize();
        var spawnPos = SpawnManager.Instance.platformSpawnPos;

        spawnPos.y += Random.Range(minSpawnRange, maxSpawnRange);
        spawnPos.x = Random.Range((-bounds.x / 2) + (enemyOffset / 2), (bounds.x / 2) - (enemyOffset / 2));

        if (SpawnManager.Instance.CheckObjectExist(spawnPos, spawnRedius, LayerMask.GetMask("Platform")))
        {
            Instantiate(enemyPrefab[randomPrefab], spawnPos, Quaternion.identity, gameObject.transform);
        }
    }
}
