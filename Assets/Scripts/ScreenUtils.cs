using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScreenUtils
{
    public static Vector2 GetWorldScreenSize()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        return new Vector2(width, height);
    }
    

    public static Vector2 GetPixelScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }

    public static float GetObjectOffset(GameObject obj)
    {
        float objectWidth = obj.GetComponent<Renderer>().bounds.size.x;
        return objectWidth;
    }
}
