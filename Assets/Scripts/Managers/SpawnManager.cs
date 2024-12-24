using System;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject easyPrefab;
        protected Vector3 platformSpawnPos;

        private void Start()
        {
            platformSpawnPos = new Vector3();
            Instantiate(easyPrefab, new Vector3(0, 1, 0), quaternion.identity, gameObject.transform);
            Instantiate(easyPrefab, new Vector3(0, 1, 0), quaternion.identity, gameObject.transform);
        }

        protected bool CheckObjectExist(Vector2 postionToSpawn, float spawnRedius, LayerMask checkleyer)
        {
            return Physics2D.OverlapCircle(postionToSpawn, spawnRedius, checkleyer) == null;
        }
    }
}