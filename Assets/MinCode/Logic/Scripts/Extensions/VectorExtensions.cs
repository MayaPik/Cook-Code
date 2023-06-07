using System;
using UnityEngine;

public static class VectorExtensions
{
    public static string GetString(this Vector2 vector)
    {
        return vector.ToString("F5");
    }

    public static string GetString(this Vector3 vector)
    {
        return vector.ToString("F5");
    }

    public static Vector3 Clamp(this Vector3 value, Vector3 minimumValue, Vector3 maximumValue)
    {
        return new Vector3(Clamp(value, minimumValue, maximumValue, vec => vec.x),
            Clamp(value, minimumValue, maximumValue, vec => vec.y),
            Clamp(value, minimumValue, maximumValue, vec => vec.z));
    }

    private static float Clamp(Vector3 value, Vector3 minimum, Vector3 maximum, Func<Vector3, float> valueGetter)
    {
        return Mathf.Clamp(valueGetter(value), valueGetter(minimum), valueGetter(maximum));
    }
}
