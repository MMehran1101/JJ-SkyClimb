using UnityEngine;
using Utilities;

namespace Spawners
{
    public class Cloud : MonoBehaviour
    {
        void Update()
        {
            if(gameObject.transform.position.y < ScreenUtils.GetCameraSize().y)
                Destroy(gameObject,1);
        }
    }
}
