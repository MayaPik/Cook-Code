using System;
using UnityEngine;

public class LerpScaler : BehaviorBase
{
    public Vector3Reference myScale;
    public FloatReference minimumAxisesScale;
    public FloatReference maximumAxisesScale;
    public float scaleTimeSeconds;
    private bool isScaling;
    private Vector3 targetScale;
    private float passedScaleTime;

    private void Awake()
    {
        myScale.SetValue(transform.localScale);
        passedScaleTime = 0f;
    }

    public void Scale(float scaleDelta)
    {
        var baseScale = targetScale == default
            ? transform.localScale
            : targetScale;

        targetScale = new Vector3(
                baseScale.x + scaleDelta,
                baseScale.y + scaleDelta,
                baseScale.z + scaleDelta);

        var minimumScale = Vector3.one * minimumAxisesScale;
        var maximumScale = Vector3.one * maximumAxisesScale;

        if (minimumScale != default)
        {
            targetScale = targetScale.Clamp(minimumScale, targetScale);
        }

        if (maximumScale != default)
        {
            targetScale = targetScale.Clamp(targetScale, maximumScale);
        }

        isScaling = true;

        PrintDebugInfo($"Starting to scale from {transform.localScale.GetString()} to {targetScale.GetString()}, delta: {scaleDelta.GetString()}, baseScale: {baseScale.GetString()}");
    }

    private void Update()
    {
        if (isScaling)
        {
            (isScaling, passedScaleTime) = transform.Lerp(
                transform.localScale,
                targetScale,
                value => transform.localScale = value,
                passedScaleTime,
                scaleTimeSeconds,
                debugMethod: PrintDebugInfo);

            myScale?.SetValue(transform.localScale);

            if (!isScaling)
            {
                targetScale = transform.localScale;
                passedScaleTime = 0f;
            }
        }
    }
}
