using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRedius;
    [SerializeField] private float minSpawnRange;
    [SerializeField] private float maxSpawnRange;
    [SerializeField] private GameObject[] NormalPrefabs;
    [SerializeField] private GameObject[] WeakPrefabs;
    [SerializeField] private GameObject[] PowerUPPrefabs;
    [SerializeField] private GameObject[] MoverPrefabs;
    private float platformOffset;
    private List<GameObject> allPlatforms;

    private void Start()
    {
        var spawnPos = SpawnManager.Instance.platformSpawnPos;
        var bounds = ScreenUtils.GetWorldScreenSize();
        platformOffset = ScreenUtils.GetObjectOffset(NormalPrefabs[0]);

        allPlatforms = new List<GameObject>();
        foreach (var t in NormalPrefabs)
        {
            allPlatforms.Add(t);
        }

        foreach (var t in WeakPrefabs)
        {
            allPlatforms.Add(t);
        }

        foreach (var t in PowerUPPrefabs)
        {
            allPlatforms.Add(t);
        }

        foreach (var t in MoverPrefabs)
        {
            allPlatforms.Add(t);
        }

        allPlatforms = allPlatforms.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < allPlatforms.Count;)
        {
            spawnPos.y += Random.Range(minSpawnRange, maxSpawnRange);
            spawnPos.x = Random.Range((-bounds.x / 2) + (platformOffset / 2), (bounds.x / 2) - (platformOffset / 2));

            if (SpawnManager.Instance.CheckObjectExist(spawnPos, spawnRedius, LayerMask.GetMask("Platform")))
            {
                Instantiate(allPlatforms[i], spawnPos, Quaternion.identity, gameObject.transform);
                SpawnManager.Instance.platformSpawnPos = spawnPos;

                i++;
            }
            else continue;
        }
    }

    private void Update()
    {
        if (gameObject.transform.childCount <=0)
        {
            SpawnManager.Instance.DecreasePlatformWavesCount(1);
            Destroy(gameObject);
        }
    }
}