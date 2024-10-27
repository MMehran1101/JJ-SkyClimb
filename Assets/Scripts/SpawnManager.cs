using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("List of Objects")] [SerializeField]
    private GameObject[] platforms;

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] clouds;

    private bool _isInstantiate = true;
    private int _platformsCount;
    private int _cloudsCount = 200;
    private Vector2 _spawnPos;

    private void Start()
    {
        InstantiateCloudes();
        _spawnPos = new Vector2(0, -2);
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
            cloudPos.y += Random.Range(1.0f, 3.0f);
            cloudPos.x = Random.Range(-2.5f, 2.5f);
            Instantiate(clouds[Random.Range(0, 8)], cloudPos
                , quaternion.identity);
        }
    }

    private IEnumerator InstantiateRoutine()
    {
        _isInstantiate = false;

        // Instantiate Normal platform
        NormalPlatform();

        // Mover And Weak Platform Instantiate
        WeakandMoverPlatform();

        yield return new WaitForSeconds(1);

        // Enemies instantiate
        EnemyInstantiate();
        
        _platformsCount += 10;
        _isInstantiate = true;
    }

    private void EnemyInstantiate()
    {
        //_spawnPos.y += Random.Range(-1f, 0f);
        _spawnPos.y += Random.Range(.5f, 1);
        _spawnPos.x = Random.Range(-2f, 2f);
        Instantiate(platforms[0], _spawnPos, quaternion.identity);
        if (_platformsCount >= 50)
        {
            _spawnPos.y += Random.Range(1f, 1.5f);
            _spawnPos.x = Random.Range(-2f, 2f);
            Instantiate(enemies[Random.Range(0, 2)], _spawnPos, quaternion.identity);
        }
    }

    private void WeakandMoverPlatform()
    {
        _spawnPos.y += Random.Range(.5f, 1);
        _spawnPos.x = Random.Range(-2f, 2f);
        Instantiate(platforms[Random.Range(1, 3)], _spawnPos, quaternion.identity);
    }

    private void NormalPlatform()
    {
        int normalCount = Random.Range(5, 10);
        for (int i = 0; i < normalCount; i++)
        {
            _spawnPos.y += Random.Range(.5f, 1);
            _spawnPos.x = Random.Range(-2f, 2f);
            Instantiate(platforms[0], _spawnPos, quaternion.identity);
        }
    }
}