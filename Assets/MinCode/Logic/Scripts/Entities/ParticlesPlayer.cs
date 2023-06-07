using UnityEngine;
using System.Collections;

/// <summary>
/// Plays particles from a prefab
/// </summary>
public class ParticlesPlayer : BehaviorBase
{
    public BooleanReference playOnce = BooleanReference.Create(true);
    public FloatReference minTimeBetweenPlays = FloatReference.Create<FloatReference>(0.01f);
    public ParticleSystem particlesPrefab;
    public Vector3Reference playAt;
    private ParticleSystem generated;

    public void StopAndClear()
    {
        if (generated != null)
        {
            generated.Stop();
            generated.Clear();
        }
    }

    public void CreateAndPlay()
    {
        if (!playOnce || generated == null || generated.transform.position != playAt || generated.time >= minTimeBetweenPlays)
        {
            generated = particlesPrefab.PlayIndependent(this, playAt);
        }
    }
}
