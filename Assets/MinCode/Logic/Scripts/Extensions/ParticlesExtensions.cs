using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticlesExtensions
{
    /// <summary>
    /// Plays particles regardless of a specific game object
    /// </summary>
    /// <param name="particles">Particles to instantiate</param>
    /// <param name="caller">Originator script</param>
    /// <param name="position">Where to play the particles in the world</param>
    /// <param name="parent">Attach to a parent - optional</param>
    /// <param name="playDelay">Delay before particles play - optional</param>
    /// <returns>The instantiated ParticleSystem</returns>
    public static ParticleSystem PlayIndependent(this ParticleSystem particles, MonoBehaviour caller, Vector3 position, Transform parent = null, float playDelay = 0f)
    {
        if (particles != null)
        {
            var toPlay = UnityEngine.Object.Instantiate(particles);
            var toPlayMain = toPlay.main;

            toPlay.transform.position = position;
            toPlay.name = $"{particles.name}Particles-{caller.name}";
            toPlayMain.startDelay = new ParticleSystem.MinMaxCurve() { constant = toPlayMain.startDelay.constant + playDelay };

            if (parent != null)
            {
                toPlay.transform.SetParent(parent);
            }

            toPlay.Play();

            return toPlay;
        }

        return null;
    }
}
