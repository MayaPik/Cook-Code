using System.Linq;
using UnityEngine;

public static class TouchInput
{
    public static Ray GetInputRay()
    {
        var screenPosition = GetScreenInputPosoition();

        return Camera.main.ScreenPointToRay(screenPosition);
    }

    public static Vector3 GetWorldInputPosoition(float z)
    {
        var screenPosition = (Vector3)GetScreenInputPosoition();
        screenPosition.z = z;

        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    public static Vector2 GetScreenInputPosoition(TouchPhase? phase = null)
    {
        Vector2 position;

        if (Input.touchCount > 0)
        {
            if (Input.touches.Any(t => !phase.HasValue || t.phase == phase.Value))
            {
                position = Input.touches.First(t => !phase.HasValue || t.phase == phase.Value).position;
            }
            else
            {
                position = Input.mousePosition;
            }
        }
        else
        {
            position = Input.mousePosition;
        }

        return position;
    }
}
