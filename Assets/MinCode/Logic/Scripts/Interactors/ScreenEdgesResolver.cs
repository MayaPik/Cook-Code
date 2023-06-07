using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class ScreenEdges
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public override string ToString()
    {
        return $"minX: {minX}, minY: {minY}, maxX: {maxX}, maxY: {maxY}";
    }
}

public static class ScreenEdgesResolver
{
    public static ScreenEdges CalculateWorldEdges()
    {
        var screenVector2 = new Vector2(Screen.width, Screen.height);
        var worldTopRight = Camera.main.ScreenToWorldPoint(screenVector2);

        return new ScreenEdges
        {
            minX = -worldTopRight.x,
            minY = -worldTopRight.y,
            maxX = worldTopRight.x,
            maxY = worldTopRight.y,
        };
    }
}
