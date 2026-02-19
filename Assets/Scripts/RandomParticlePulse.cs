using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PerInstanceParticleRandomizer : MonoBehaviour
{
    [Tooltip("Optional small startup delay so systems instantiated in same frame don't sync")]
    public float maxStartupStagger = 0.05f;

    void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        if (ps == null) return;

        // Stop first so we can set seed/parameters safely
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // Build a pretty-unique seed using instanceID + time + random
        uint seed = (uint)(Mathf.Abs(GetInstanceID()) ^ (int)(Time.realtimeSinceStartup * 100000f) ^ Random.Range(1, int.MaxValue));
        ps.randomSeed = seed;
        ps.useAutoRandomSeed = false; // make sure it uses our seed

        // Option: small stagger so multiple instances in same frame don't start identical
        float stagger = Random.Range(0f, maxStartupStagger);
        if (stagger > 0f) StartCoroutine(PlayAfterDelay(ps, stagger));
        else ps.Play();
    }

    System.Collections.IEnumerator PlayAfterDelay(ParticleSystem ps, float delay)
    {
        yield return new WaitForSeconds(delay);
        ps.Play();
    }
}

