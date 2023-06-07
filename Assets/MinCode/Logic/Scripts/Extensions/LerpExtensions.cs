using System;
using UnityEngine;

public static class LerpExtensions
{
    public static (bool isAtTarget, float timePassed) Lerp(
        this Transform transform,
        Vector3 currentValue,
        Vector3 targetValue,
        Action<Vector3> valueSetter,
        float timePassed,
        float timeToTarget,
        float targetTolerance = 0.0001f,
        Action<string> debugMethod = null)
    {
        var hasMoreToScale = true;
        var updatedTimePassed = timePassed;

        if (currentValue != targetValue)
        {
            updatedTimePassed = timePassed + Time.deltaTime;

            if (Vector3.Distance(currentValue, targetValue) <= targetTolerance)
            {
                debugMethod?.Invoke($"got to target vector of {targetValue.GetString()}");
                valueSetter.Invoke(targetValue);
                hasMoreToScale = false;
            }
            else
            {
                var progress = updatedTimePassed / timeToTarget;
                var newValue = Vector3.Lerp(currentValue, targetValue, progress);
                debugMethod?.Invoke($"lerping from {currentValue.GetString()} -> {targetValue.GetString()}, timePassed: {updatedTimePassed.GetString()}, timeToTarget: {timeToTarget.GetString()}, progress: {(progress * 100f).GetString()}%");
                valueSetter.Invoke(newValue);
                debugMethod?.Invoke($"set value to {newValue.GetString()}");
                hasMoreToScale = true;
            }
        }

        return (hasMoreToScale, updatedTimePassed);
    }
}
