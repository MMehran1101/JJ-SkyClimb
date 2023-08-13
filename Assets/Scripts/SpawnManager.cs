using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Platforms")]
    [SerializeField] private GameObject[] platforms;
    

    private int _platformCount = 300;

    private void Start()
    {
        PlatformInstantiate();
    }

    private void PlatformInstantiate()
    {
        var platformPos = new Vector2();

        for (var i = 0; i < _platformCount; i++)
        {
            platformPos.y += Random.Range(.3f, 1);
            platformPos.x = Random.Range(-2f, 2f);
            Instantiate(platforms[1], platformPos, quaternion.identity);
        }
    }
}