using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Platforms")] 
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private GameObject[] enemies;

    private bool _isInstantiate = true;
    private int _platformCount;
    private Vector2 _spawnPos;

    private void Start()
    {
        _spawnPos = new Vector2(0,-2);
        StartCoroutine(InstantiateRoutine());
    }

    private void Update()
    {
        if (_isInstantiate && _platformCount <= 500) StartCoroutine(InstantiateRoutine());
    }


    private IEnumerator InstantiateRoutine()
    {
        _isInstantiate = false;
        
        // Instantiate Normal platform
        var normalCount = Random.Range(5, 10);
        for (var i = 0; i < normalCount; i++)
        {
            _spawnPos.y += Random.Range(.5f, 1);
            _spawnPos.x = Random.Range(-2f, 2f);
            Instantiate(platforms[0], _spawnPos, quaternion.identity);
        }

        // Mover And Weak Platform Instantiate
        _spawnPos.y += Random.Range(.5f, 1);
        _spawnPos.x = Random.Range(-2f, 2f);
        Instantiate(platforms[Random.Range(1, 3)], _spawnPos, quaternion.identity);
        _platformCount += 10;
        yield return new WaitForSeconds(1);
        
        // Enemies instantiate
        if (_platformCount>=50)
        {
            _spawnPos.y += Random.Range(1f, 2f);
            _spawnPos.x = Random.Range(-2f, 2f);
            Instantiate(enemies[Random.Range(0,2)], _spawnPos, quaternion.identity);
        }

        _isInstantiate = true;
    }
}