using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnTest : MonoBehaviour
    {
        private enum DifficultState
        {
            Easy,
            Medium,
            Hard,
        }

        private enum PlatformType
        {
            Normal,
            Weak,
            Mover
        }

        [Header("Platform Settings")] [SerializeField]
        private GameObject normalPlatform;

        [SerializeField] private GameObject weakPlatform;
        [SerializeField] private GameObject moverPlatform;
        [SerializeField] private GameObject easyyyyyyyy;

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
            Instantiate(easyyyyyyyy, new Vector3(0, 1, 0), quaternion.identity);
            Instantiate(easyyyyyyyy, new Vector3(0, 1, 0), quaternion.identity);
            Instantiate(easyyyyyyyy, new Vector3(0, 1, 0), quaternion.identity);
            Instantiate(easyyyyyyyy, new Vector3(0, 1, 0), quaternion.identity);
            Instantiate(easyyyyyyyy, new Vector3(0, 1, 0), quaternion.identity);
            Instantiate(easyyyyyyyy, new Vector3(0, 1, 0), quaternion.identity);
            Instantiate(easyyyyyyyy, new Vector3(0, 1, 0), quaternion.identity);
            //SpawnPlatform();
        }

        public void SpawnPlatform()
        {
            PlatformType nextPlatformType = GetNextPlatformType();
            GameObject platformPrefab = GetPlatformPrefab(nextPlatformType);

            // Location spawn of platform
            _spawnPos.y += Random.Range(0f, .5f);
            _spawnPos.x = Random.Range((-bounds.x / 2) + (platformOffset/2), (bounds.x / 2) - (platformOffset/2));

            switch (GameManager.Instance.GetScore())
            {
                case < 500:
                    if (CheckObjectExist(_spawnPos, DifficultSettings(DifficultState.Easy)
                            , LayerMask.GetMask("Platform")))
                    {
                        Instantiate(platformPrefab, _spawnPos, quaternion.identity);
                        platformCount += 1;
                    }

                    break;
                case > 500 and < 1500:
                    if (CheckObjectExist(_spawnPos, DifficultSettings(DifficultState.Medium)
                            , LayerMask.GetMask("Platform")))
                    {
                        Instantiate(platformPrefab, _spawnPos, quaternion.identity);
                        platformCount += 1;
                    }

                    break;

                case > 1500:
                    if (CheckObjectExist(_spawnPos, DifficultSettings(DifficultState.Hard)
                            , LayerMask.GetMask("Platform")))
                    {
                        Instantiate(platformPrefab, _spawnPos, quaternion.identity);
                        platformCount += 1;
                    }

                    break;
            }


            AddToHistory(nextPlatformType);

            if (platformCount <= 500)
            {
                Invoke(nameof(SpawnPlatform), .1f);
            }
        }

        PlatformType GetNextPlatformType()
        {
            var a = Random.Range(1, 10);
            int[] normalP = { 1, 3, 8, 5, 7, 9 };
            int[] weakP = { 2, 4 };
            int[] moverP = { 6 };

            if (normalP.Contains(a))
            {
                return PlatformType.Normal;
            }
            else if (weakP.Contains(a) && CountInHistory(PlatformType.Weak) <= 1)
            {
                return PlatformType.Weak;
            }
            else if (moverP.Contains(a))
            {
                return PlatformType.Mover;
            }

            return PlatformType.Normal;
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
}