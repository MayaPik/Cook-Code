using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A controller for objects that can be collected (a coin for example)
/// </summary>
public class CollectableController : BehaviorBase
{
    [Header("Optional. Particles to play once collected")]
    public ParticleSystem collectedParticles;
    public UnityEvent onReset;
    public UnityEvent onCollected;
    private bool collected;
    private Vector3 startScale;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    public void Collect()
    {
        PrintDebugInfo($"collected! collected: {collected}");
        if (!collected)
        {
            PrintDebugInfo("i was collected");
            collectedParticles.PlayIndependent(this, transform.position);
            collected = true;
            onCollected.Invoke();
        }
    }

    public void Reset()
    {
        onReset.Invoke();
        transform.position = startPosition;
        transform.localScale = startScale;
        collected = false;
    }

    public void Destroy()
    {
        if (this != null)
        {
            Destroy(gameObject);
        }
    }
}
