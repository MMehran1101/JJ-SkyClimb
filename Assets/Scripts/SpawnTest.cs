using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnTest : MonoBehaviour
{
    private enum DifficultState
    {
        Easy,
        Medium,
        Hard
    }
    private enum PlatformType
    {
        Normal,
        Weak,
        Mover
    }

    [Header("Platform Settings")] 
    [SerializeField] private GameObject normalPlatform;
    [SerializeField] private GameObject weakPlatform;
    [SerializeField] private GameObject moverPlatform;
    private List<PlatformType> spawnHistory = new List<PlatformType>();

    private Vector2 _spawnPos;
    private Vector2 bounds;
    private float platformOffset;
    private int spawnHistoryLimit = 3;
    private int platformCount;

    private void Start()
    {
        bounds = ScreenUtils.GetWorldScreenSize();
        platformOffset = ScreenUtils.GetObjectOffset(normalPlatform);
        //Spawn();
        SpawnPlatform();
    }


    void Spawn()
    {
        platformCount = 50;
        for (int i = 0; i <= platformCount;)
        {
            _spawnPos.y += Random.Range(0f, .5f);
            _spawnPos.x = Random.Range((-bounds.x / 2) + platformOffset, (bounds.x / 2) - platformOffset);
            if (CheckObjectExist(_spawnPos, DifficultSettings(DifficultState.Easy), LayerMask.GetMask("Platform")))
            {
                Instantiate(normalPlatform, _spawnPos, quaternion.identity);
                i++;
            }
        }

        for (int i = 0; i <= platformCount; i++)
        {
            _spawnPos.y += Random.Range(0f, 1f);
            if (CheckObjectExist(_spawnPos, DifficultSettings(DifficultState.Medium), LayerMask.GetMask("Platform")))
            {
                Instantiate(normalPlatform, _spawnPos, quaternion.identity);
                i++;
            }
        }
    }
    void SpawnPlatform()
    {
        PlatformType nextPlatformType = GetNextPlatformType();
        GameObject platformPrefab = GetPlatformPrefab(nextPlatformType);

        // Location spawn of platform
        _spawnPos.y += Random.Range(0f, .5f);
        _spawnPos.x = Random.Range((-bounds.x / 2) + platformOffset, (bounds.x / 2) - platformOffset);
        
        if (CheckObjectExist(_spawnPos, DifficultSettings(DifficultState.Easy), LayerMask.GetMask("Platform")))
        {
            Instantiate(platformPrefab, _spawnPos, quaternion.identity);
            platformCount += 1;
        }

        AddToHistory(nextPlatformType);

        if (platformCount <= 200)
        {
            Invoke(nameof(SpawnPlatform), 0.1f);
        }
    }

    PlatformType GetNextPlatformType()
    {
        PlatformType nextType;
        do
        {
            nextType = (PlatformType)Random.Range(0, 3);
        } 
        while (nextType == PlatformType.Weak && CountInHistory(PlatformType.Weak) >= 1);

        return nextType;
    }

    GameObject GetPlatformPrefab(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Normal:
                return normalPlatform;
            case PlatformType.Weak:
                return weakPlatform;
            case PlatformType.Mover:
                return moverPlatform;
            default:
                return normalPlatform;
        }
    }

    void AddToHistory(PlatformType type)
    {
        spawnHistory.Add(type);
        if (spawnHistory.Count > spawnHistoryLimit)
        {
            spawnHistory.RemoveAt(0);
        }
    }

    int CountInHistory(PlatformType type)
    {
        int count = 0;
        foreach (PlatformType t in spawnHistory)
        {
            if (t == type) count++;
        }
        return count;
    }



    private float DifficultSettings(DifficultState state)
    {
        switch (state)
        {
            case DifficultState.Easy:
                return 0.5f;
            case DifficultState.Medium:
                return 1f;
            case DifficultState.Hard:
                return 1.5f;
        }

        return 1;
    }

    private bool CheckObjectExist(Vector2 postionToSpawn, float spawnRedius, LayerMask checkleyer)
    {
        return Physics2D.OverlapCircle(postionToSpawn, spawnRedius, checkleyer) == null;
    }
}